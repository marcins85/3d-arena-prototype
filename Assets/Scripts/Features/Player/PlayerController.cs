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
    private ITurnHandler _turnHandler;
    private IAnimationSystem _animation;

    private bool _isJumpAnimationPlaying;
    private bool _jumpRequest = false;
    private Vector2 _moveInput;

    public void Inject(IMovement movement, IRotation rotation, IJump jump, ITurnHandler turnHandler, IPlayerInput input, IAnimationSystem animation)
    {
        _movement = movement;
        _rotation = rotation;
        _jump = jump;
        _input = input;
        _turnHandler = turnHandler;
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

        _animation.Update(_moveInput, grounded, _movement.CurrentVerticalVelocity, _jumpRequest);
        _jumpRequest = false;
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

        if (velocity.sqrMagnitude < 0.01f)
        {
            _movement.SetSprintTrigger(false);
        }
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
        if (_isJumpAnimationPlaying) return;

        _isJumpAnimationPlaying = true;
        _jumpRequest = true;
        //_animation.PlayJump();
    }

    public void OnJumpTakeOff()
    {
        //_jump.SetJumpTrigger(true);
        _animation.OnJumpTakeOff();
    }

    public void OnJumpLanding()
    {
        //_jump.SetVerticalVelocity(0f);
        _animation.OnJumpLanding();
    }

    public void OnJumpFinished()
    {
        _jump.SetJumpTrigger(false);
        _isJumpAnimationPlaying = false;
        _animation.OnJumpFinished();
        //_jumpRequest = false;
    }

    public void OnTurnLeftFinished()
    {
        _turnHandler?.OnTurnFinished(false);
    }

    public void OnTurnRightFinished()
    {
        _turnHandler?.OnTurnFinished(true);
    }

    public PlayerConfigSO GetPlayerConfigSO() => _config;
    public CharacterController GetCharacterController() => _characterController;
    public InputActionAsset GetInputActionAsset() => _asset;
    public Transform GetCamRoot() => _camRoot;
    public Transform GetCamPitch() => _camPitch;
    public Animator GetAnimator() => _animator;
    public LayerMask GetLayerMask() => _groundMask;
}