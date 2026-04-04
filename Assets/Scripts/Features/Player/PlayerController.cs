using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

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
    private IAnimationSystem _animation;

    private bool _jumpRequest = false;
    private Vector2 _moveInput;

    public void Inject(IMovement movement, IRotation rotation, IJump jump, ITurnHandler turnHandler, IPlayerInput input, IAnimationSystem animation)
    {
        _movement = movement;
        _rotation = rotation;
        _jump = jump;
        _input = input;
        _animation = animation;
    }

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        bool grounded = _movement.IsGroundedRaycast();
        _jump.HandleJump(grounded);
        float y = _jump.GetVerticalVelocity();
        _movement.HandleMovement(y);
        _jump.SetVerticalVelocity(_movement.CurrentVerticalVelocity);
        _rotation.HandleRotation();

        bool jumpRequested = _jumpRequest;
        _jumpRequest = false;
        _animation.Update(_moveInput, grounded, _movement.CurrentVerticalVelocity, jumpRequested);
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.OnMove += OnMove;
        _input.OnLook += _rotation.SetLookInput;
        _input.OnSprint += OnSprint;
        _input.OnJump += OnJump;
    }
    private void OnDisable()
    {
        _input.OnMove -= OnMove;
        _input.OnLook -= _rotation.SetLookInput;
        _input.OnSprint -= OnSprint;
        _input.OnJump -= OnJump;

        _input.Disable();
    }

    private void OnMove(Vector2 velocity)
    {
        _moveInput = velocity;

        _movement.SetMoveInput(velocity);
        _rotation.SetMoveInput(velocity);
    }

    private void OnSprint(bool sprint)
    {
        _movement.SetSprintTrigger(sprint);
        _animation.SetSprint(sprint);
    }

    private void OnJump(bool jump)
    {
        if (!jump) return;
        if (_rotation.IsTurning) return;
        if (!_jump.CanJump) return;

        _jumpRequest = true;
    }

    public void OnJumpTakeOff()
    {
        _animation.OnJumpTakeOff();
    }

    public void OnJumpLanding()
    {
        _animation.OnJumpLanding();
    }

    public void OnJumpFinished()
    {
        _animation.OnJumpFinished();
    }

    public void OnTurnLeftFinished()
    {
        _animation.OnTurnLeftFinished();
    }

    public void OnTurnRightFinished()
    {
        _animation.OnTurnRightFinished();
    }

    public PlayerConfigSO GetPlayerConfigSO() => _config;
    public CharacterController GetCharacterController() => _characterController;
    public InputActionAsset GetInputActionAsset() => _asset;
    public Transform GetCamRoot() => _camRoot;
    public Transform GetCamPitch() => _camPitch;
    public Animator GetAnimator() => _animator;
    public LayerMask GetLayerMask() => _groundMask;
}