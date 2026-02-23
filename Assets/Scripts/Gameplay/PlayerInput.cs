using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionAsset _asset;
    [SerializeField] private string _actionMap = "Main";
    [SerializeField] private string _move = "Move";
    [SerializeField] private string _look = "Look";
    [SerializeField] private string _jump = "Jump";

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTrigger {  get; private set; }

    private void Awake()
    {
        InputActionMap map = _asset.FindActionMap(_actionMap);
        _moveAction = map.FindAction(_move);
        _lookAction = map.FindAction(_look);
        _jumpAction = map.FindAction(_jump);
    }

    private void SubscribeActions()
    {
        _moveAction.performed += input => MoveInput = input.ReadValue<Vector2>();
        _moveAction.canceled += _ => MoveInput = Vector2.zero;

        _lookAction.performed += input => LookInput = input.ReadValue<Vector2>();
        _lookAction.canceled += _ => LookInput = Vector2.zero;

        _jumpAction.performed += _ => JumpTrigger = true;
        _jumpAction.canceled += _ => JumpTrigger = false;
    }

    private void UnsubscribeActions()
    {
        _moveAction.performed -= input => MoveInput = input.ReadValue<Vector2>();
        _moveAction.canceled -= _ => MoveInput = Vector2.zero;

        _lookAction.performed -= input => LookInput = input.ReadValue<Vector2>();
        _lookAction.canceled -= _ => LookInput = Vector2.zero;

        _jumpAction.performed -= _ => JumpTrigger = true;
        _jumpAction.canceled -= _ => JumpTrigger = false;
    }

    private void OnEnable()
    {
        SubscribeActions();
        _asset.FindActionMap(_actionMap).Enable();
    }

    private void OnDisable()
    {
        UnsubscribeActions();
        _asset.FindActionMap(_actionMap).Disable();
    }
}
