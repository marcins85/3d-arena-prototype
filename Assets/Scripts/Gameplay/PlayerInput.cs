using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset _asset;
    [SerializeField] private string _actionMap = "Main";
    [SerializeField] private string _move = "Move";
    [SerializeField] private string _look = "Look";
    [SerializeField] private string _jump = "Jump";
    [SerializeField] private string _sprint = "Sprint";

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _sprintAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTrigger {  get; private set; }
    public bool SprintTrigger { get; private set; }

    private void Awake()
    {
        InputActionMap map = _asset.FindActionMap(_actionMap);
        _moveAction = map.FindAction(_move);
        _lookAction = map.FindAction(_look);
        _jumpAction = map.FindAction(_jump);
        _sprintAction = map.FindAction(_sprint);
    }

    private void SubscribeActions()
    {
        if (_moveAction != null)
        {
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;
        }

        if (_lookAction != null)
        {
            _lookAction.performed += OnLookPerformed;
            _lookAction.canceled += OnLookCanceled;
        }

        if (_jumpAction != null)
        {
            _jumpAction.performed += OnJumpPerformed;
            _jumpAction.canceled += OnJumpCanceled;
        }

        if (_sprintAction != null)
        {
            _sprintAction.performed += OnSprintPerformed;
            _sprintAction.canceled += OnSprintCanceled;
        }
    }

    private void UnsubscribeActions()
    {
        if (_moveAction != null)
        {
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;
        }

        if (_lookAction != null)
        {
            _lookAction.performed -= OnLookPerformed;
            _lookAction.canceled -= OnLookCanceled;
        }

        if (_jumpAction != null)
        {
            _jumpAction.performed -= OnJumpPerformed;
            _jumpAction.canceled -= OnJumpCanceled;
        }

        if (_sprintAction != null)
        {
            _sprintAction.performed -= OnSprintPerformed;
            _sprintAction.canceled -= OnSprintCanceled;
        }
    }

    // Input callbacks - use concrete methods so unsubscription works correctly
    private void OnMovePerformed(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    private void OnMoveCanceled(InputAction.CallbackContext _) => MoveInput = Vector2.zero;

    private void OnLookPerformed(InputAction.CallbackContext ctx) => LookInput = ctx.ReadValue<Vector2>();
    private void OnLookCanceled(InputAction.CallbackContext _) => LookInput = Vector2.zero;

    private void OnJumpPerformed(InputAction.CallbackContext _) => JumpTrigger = true;
    private void OnJumpCanceled(InputAction.CallbackContext _) => JumpTrigger = false;

    private void OnSprintPerformed(InputAction.CallbackContext _) => SprintTrigger = true;
    private void OnSprintCanceled(InputAction.CallbackContext _) => SprintTrigger = false;

    private void OnEnable()
    {
        SubscribeActions();
        _asset.FindActionMap(_actionMap).Enable();
    }

    private void OnDisable()
    {
        _asset.FindActionMap(_actionMap).Disable();
        UnsubscribeActions();
    }
}
