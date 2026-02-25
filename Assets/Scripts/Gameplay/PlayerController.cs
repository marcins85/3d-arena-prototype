using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputActionAsset _asset;
    [SerializeField] private Transform _camTarget;
    private PlayerInput _input;
    private PlayerMovement _movement;
    private PlayerRotation _rotation;
    private PlayerJump _jump;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input = new PlayerInput(_asset);

        _movement = new PlayerMovement(_characterController, transform);
        _rotation = new PlayerRotation(_camTarget, transform);
        _jump = new PlayerJump();
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