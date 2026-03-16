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

    public ITurnHandler turnHandler;
    public IJumpHandler jumpHandler;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _input = new PlayerInput(_asset);
        _movement = new PlayerMovement(_characterController, transform, _camRoot, _groundMask, _config);

        var rotation = new PlayerRotation(_camRoot, _camPitch, transform, _animator, _movement, _config);
        _rotation = rotation;
        turnHandler = rotation;

        var jump = new PlayerJump(_config);
        _jump = jump;
        jumpHandler = jump;
    }

    private void Update()
    {
        bool grounded = _movement.IsGroundedRaycast();
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
        _movement.SetMoveInput(velocity);
        ((PlayerRotation)_rotation).SetMoveInput(velocity);
        _animator.SetFloat("MoveSpeed", velocity.y);
        _animator.SetFloat("StrafeWalking", velocity.x);

        if (velocity.sqrMagnitude < 0.01f)
        {
            _movement.SetSprintTrigger(false);
            _animator.SetBool("Sprint", false);
        }

        if (velocity.y < 0.01f)
        {
            _animator.SetFloat("StrafeWalking", 0);
        }
    }

    private void OnSprint(bool sprint)
    {
        _movement.SetSprintTrigger(sprint);
        _animator.SetBool("Sprint", sprint);
    }

    private void OnJump(bool jump)
    {
        if (_rotation.IsTurning == false && _jump.CanJump == true)
        {
            StartCoroutine(JumpTimeout(jump));
        
            if (jump)
            {
                _animator.SetTrigger("Jump");
                //jumpHandler?.OnJumpStarted();
            }
        }
    }

    private IEnumerator JumpTimeout(bool value)
    {
        yield return new WaitForSeconds(0.5f);
        _jump.SetJumpTrigger(value);
    }

    public void OnJumpLanding()
    {
        _jump.SetVerticalVelocity(0f);
    }

    public void OnJumpFinished()
    {
        _jump.CanJump = true;
        //jumpHandler?.OnJumpFinished();
        _jump.SetJumpTrigger(false);
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