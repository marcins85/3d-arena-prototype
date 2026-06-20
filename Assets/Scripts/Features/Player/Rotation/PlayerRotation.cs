using System;
using UnityEngine;

public interface ITurnHandler
{
    void OnTurnFinished(bool right);
}

public class PlayerRotation : IRotation//, ITurnHandler
{
    private IEntityConfig _config;

    private Transform _player;
    private Transform _camRoot;
    private Transform _camPitch;

    private IAnimationSystem _animation;

    private float _verticalRotation;
    private Vector2 _lookInput;

    private const float RotateSpeed = 120f;
    private bool _isMoving;
    private bool _wantsToMove;
    private bool _justStartedMovingForward = false;
    
    public bool IsMoving { get => _isMoving; set => _isMoving = value; }
    public bool WantsToMove { get => _wantsToMove; set => _wantsToMove = value; }
    public bool JustStartedMovingForward { get => _justStartedMovingForward; set => _justStartedMovingForward = value; }

    public bool IsTurning { get; set; }

    public PlayerRotation(Transform camRoot, Transform camPitch, Transform player, IEntityConfig config)
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
        // bool prevWantsToMove = WantsToMove;
        // WantsToMove = moveInput != Vector2.zero;//moveInput.y > 0.1f;
        // JustStartedMovingForward = !prevWantsToMove && WantsToMove;

        // if (!WantsToMove && !IsTurning)
        //     IsMoving = false;

        bool prevWantsToMove = WantsToMove;
        float deadzone = 0.1f;
        WantsToMove = moveInput.sqrMagnitude > deadzone * deadzone;
        JustStartedMovingForward = !prevWantsToMove && WantsToMove && moveInput.y > deadzone;

        if (!WantsToMove && !IsTurning)
        {
            IsMoving = false;
            JustStartedMovingForward = false;
        }
    }

    public void HandleRotation()
    {
        Debug.Log(_config.MouseSensitivity);
        float mouseX = _lookInput.x * _config.MouseSensitivity;
        float mouseY = _lookInput.y * _config.MouseSensitivity;

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
        _verticalRotation = Mathf.Clamp(_verticalRotation - amount, -_config.UpDownLimit, _config.UpDownLimit);
        _camPitch.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    public float GetDeltaYaw()
    {
        float targetYaw = _camRoot.eulerAngles.y;
        float currentYaw = _player.eulerAngles.y;
        return Mathf.DeltaAngle(currentYaw, targetYaw);
    }

    private void HandlePlayerRotation()
    {
        float targetYaw = _camRoot.eulerAngles.y;
        float currentYaw = _player.eulerAngles.y;

        //// TURN-IN-PLACE gdy stoi
        if (!IsTurning && JustStartedMovingForward && !IsMoving) return;

        // jeśli zaczynamy iść, ale turn-in-place się nie odpaliło
        if (!IsTurning && JustStartedMovingForward && WantsToMove && !IsMoving)
        {
            IsMoving = true;
        }

        // AUTO-ROTATE — gdy idzie
        if (!IsTurning && IsMoving)
        {
            float newYaw = Mathf.MoveTowardsAngle(currentYaw, targetYaw, RotateSpeed * Time.deltaTime);
            _player.rotation = Quaternion.Euler(0, newYaw, 0);
            return;
        }
    }
}