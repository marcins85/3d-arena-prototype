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

    private Animator _animator;
    private IMovement _movement;

    private float _verticalRotation;
    private Vector2 _lookInput;

    private const float RotateSpeed = 120f;
    private const float MoveTurnTreshold = 45f;
    private bool _wantsToMoveForward;
    private bool _isMoving;

    public bool IsTurning { get; private set; }

    public PlayerRotation(Transform camRoot, Transform camPitch, Transform player, Animator animator, IMovement movement, PlayerConfigSO config)
    {
        _camRoot = camRoot;
        _camPitch = camPitch;
        _player = player;
        _animator = animator;
        _movement = movement;
        _config = config;
    }

    public void SetLookInput(Vector2 input)
    {
        _lookInput = input;
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        _isMoving = moveInput != Vector2.zero;
        _wantsToMoveForward = moveInput.y > 0.1f;
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
        if (!IsTurning && _wantsToMoveForward)
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

        // AUTO-ROTATE — gdy idzie
        if (_isMoving && !IsTurning)
        {
            float newYaw = Mathf.MoveTowardsAngle(currentYaw, targetYaw, RotateSpeed * Time.deltaTime);
            _player.rotation = Quaternion.Euler(0, newYaw, 0);
            return;
        }
    }

    public void StartTurn(bool right)
    {
        IsTurning = true;
        _movement.CanMove = false;
        _animator.SetTrigger(right ? "TurnRight" : "TurnLeft");
    }

    public void OnTurnFinished(bool right)
    {
        //float targetYaw = _camRoot.eulerAngles.y;
        //_player.rotation = Quaternion.Euler(0, targetYaw, 0);

        IsTurning = false;
        _movement.CanMove = true;
    }
}