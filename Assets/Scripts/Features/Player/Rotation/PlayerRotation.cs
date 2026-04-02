using System;
using System.Diagnostics;
using UnityEngine;

public interface ITurnHandler
{
    void OnTurnFinished(bool right);
}

public class PlayerRotation : IRotation, ITurnHandler
{
    private PlayerConfigSO _config;

    private Transform _player;
    private Transform _camRoot;
    private Transform _camPitch;

    private IAnimationSystem _animation;

    private float _verticalRotation;
    private Vector2 _lookInput;

    private const float RotateSpeed = 120f;
    private const float MoveTurnTreshold = 45f;
    private bool _isMoving = false;
    private bool _wantsToMove;
    private bool _justStartedMovingForward = false;

    public event Action<bool> OnTurnStartedEvent;
    public event Action OnTurnFinishedEvent;

    public bool IsTurning { get; set; }

    public PlayerRotation(Transform camRoot, Transform camPitch, Transform player, PlayerConfigSO config)
    {
        _camRoot = camRoot;
        _camPitch = camPitch;
        _player = player;
        _config = config;
    }

    public void SetLookInput(Vector2 input)
    {
        _lookInput = input;
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        bool prevWantsToMove = _wantsToMove;

        _wantsToMove = moveInput != Vector2.zero;//moveInput.y > 0.1f;

        _justStartedMovingForward = !prevWantsToMove && _wantsToMove;

        if (!_wantsToMove && !IsTurning)
            _isMoving = false;

    }

    public void HandleRotation()
    {
        float mouseX = _lookInput.x * _config.mouseSensitivity;
        float mouseY = _lookInput.y * _config.mouseSensitivity;

        RotateCameraYaw(mouseX);
        RotateCameraPitch(mouseY);

        HandlePlayerRotation();
    }

    private void RotateCameraYaw(float amount)
    {
        _camRoot.Rotate(0f, amount, 0f, Space.World);
    }

    private void RotateCameraPitch(float amount)
    {
        _verticalRotation = Mathf.Clamp(_verticalRotation - amount, -_config.upDownLimit, _config.upDownLimit);
        _camPitch.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    private void HandlePlayerRotation()
    {
        float targetYaw = _camRoot.eulerAngles.y;
        float currentYaw = _player.eulerAngles.y;
        float delta = Mathf.DeltaAngle(currentYaw, targetYaw);

        // TURN-IN-PLACE gdy stoi
        if (!IsTurning && _justStartedMovingForward && !_isMoving)
        {
            if (delta > MoveTurnTreshold)
            {
                StartTurn(true);
                return;
            }
            else if (delta < -MoveTurnTreshold)
            {
                StartTurn(false);
                return;
            }
        }

        // jeśli zaczynamy iść, ale turn-in-place się nie odpaliło
        if (!IsTurning && _justStartedMovingForward && _wantsToMove && !_isMoving)
        {
            _isMoving = true;
        }

        // AUTO-ROTATE — gdy idzie
        if (!IsTurning && _isMoving)
        {
            float newYaw = Mathf.MoveTowardsAngle(currentYaw, targetYaw, RotateSpeed * Time.deltaTime);
            _player.rotation = Quaternion.Euler(0, newYaw, 0);
            return;
        }
    }

    public void StartTurn(bool right)
    {
        IsTurning = true;
        OnTurnStartedEvent?.Invoke(right);
    }

    public void OnTurnFinished(bool right)
    {
        IsTurning = false;
        _isMoving = _wantsToMove;
        OnTurnFinishedEvent?.Invoke();
    }
}