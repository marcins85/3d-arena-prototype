using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO _config;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputActionAsset _asset;
    [SerializeField] private Transform _camTarget;
    [SerializeField] private Transform _rotator;
    private IPlayerInput _input;
    private IMovement _movement;
    private IRotation _rotation;
    private IJump _jump;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input = new PlayerInput(_asset);

        _movement = new PlayerMovement(_characterController, transform, _config);
        _rotation = new PlayerRotation(_camTarget, transform, _config);
        _jump = new PlayerJump(_config);
    }

    private void Update()
    {
        bool grounded = _characterController.isGrounded;
        _jump.HandleJump(grounded);
        float y = _jump.GetVerticalVelocity();
        _movement.HandleMovement(y);
        _jump.SetVerticalVelocity(_movement.CurrentVerticalVelocity);
        _rotation.HandleRotation();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.OnMove += _movement.SetMoveInput;
        _input.OnLook += _rotation.SetLookInput;
        _input.OnSprint += _movement.SetSprintTrigger;
        _input.OnJump += _jump.SetJumpTrigger;
    }
    private void OnDisable()
    {
        _input.OnMove -= _movement.SetMoveInput;
        _input.OnLook -= _rotation.SetLookInput;
        _input.OnSprint -= _movement.SetSprintTrigger;
        _input.OnJump -= _jump.SetJumpTrigger;

        _input.Disable();
    }
}