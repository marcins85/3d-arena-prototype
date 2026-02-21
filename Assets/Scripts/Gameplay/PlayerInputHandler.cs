using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private string actionMapName = "Main";
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTrigger { get; private set; }
    public bool SprintTrigger { get; private set; }

    private void Awake()
    {
        InputActionMap mapRefernece = inputActionAsset.FindActionMap(actionMapName);
        moveAction = mapRefernece.FindAction(move);
        lookAction = mapRefernece.FindAction(look);
        jumpAction = mapRefernece.FindAction(jump);
        sprintAction = mapRefernece.FindAction(sprint);
    }

    private void SubscribeActions()
    {
        moveAction.performed += (input) => MoveInput = input.ReadValue<Vector2>();
        moveAction.canceled += (_) => MoveInput = Vector2.zero;

        lookAction.performed += (input) => LookInput = input.ReadValue<Vector2>();
        lookAction.canceled += (_) => LookInput = Vector2.zero;

        jumpAction.performed += (_) => JumpTrigger = true;
        jumpAction.canceled += (_) => JumpTrigger = false;

        sprintAction.performed += (_) => SprintTrigger = true;
        sprintAction.canceled += (_) => SprintTrigger = false;
    }

    private void UnsubscribeActions()
    {
        moveAction.performed -= (input) => MoveInput = input.ReadValue<Vector2>();
        moveAction.canceled -= (_) => MoveInput = Vector2.zero;

        lookAction.performed -= (input) => LookInput = input.ReadValue<Vector2>();
        lookAction.canceled -= (_) => LookInput = Vector2.zero;

        jumpAction.performed -= (_) => JumpTrigger = true;
        jumpAction.canceled -= (_) => JumpTrigger = false;

        sprintAction.performed -= (_) => SprintTrigger = true;
        sprintAction.canceled -= (_) => SprintTrigger = false;
    }

    private void OnEnable()
    {
        SubscribeActions();
        inputActionAsset.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        UnsubscribeActions();
        inputActionAsset.FindActionMap(actionMapName).Disable();
    }
}
