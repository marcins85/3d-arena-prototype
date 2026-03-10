using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO _config;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private InputActionAsset _asset;
    [SerializeField] private Transform _camRoot;
    [SerializeField] private Transform _camPitch;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundMask;
    private IPlayerInput _input;
    private IMovement _movement;
    private IRotation _rotation;
    private IJump _jump;

    public ITurnHandler turnHandler;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input = new PlayerInput(_asset);
        _movement = new PlayerMovement(_characterController, transform, _camRoot, _groundMask, _config);

        var l_rotation = new PlayerRotation(_camRoot, _camPitch, transform, _animator, _movement, _config);
        _rotation = l_rotation;
        turnHandler = l_rotation;

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

        _input.OnMove += OnMove;
        _input.OnLook += _rotation.SetLookInput;
        _input.OnSprint += _movement.SetSprintTrigger;
        _input.OnJump += _jump.SetJumpTrigger;
    }
    private void OnDisable()
    {
        _input.OnMove -= OnMove;
        _input.OnLook -= _rotation.SetLookInput;
        _input.OnSprint -= _movement.SetSprintTrigger;
        _input.OnJump -= _jump.SetJumpTrigger;

        _input.Disable();
    }

    private void OnMove(Vector2 velocity)
    {
        _movement.SetMoveInput(velocity);
        ((PlayerRotation)_rotation).SetMoveInput(velocity);
        _animator.SetFloat("MoveSpeed", velocity.y);
    }

    public void OnTurnLeftFinished()
    {
        turnHandler?.OnTurnFinished(false);
    }

    public void OnTurnRightFinished()
    {
        turnHandler?.OnTurnFinished(true);
    }
}