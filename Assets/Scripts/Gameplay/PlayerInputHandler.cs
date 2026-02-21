using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private string actionMapName = "Main";
    [SerializeField] private string movement = "Move";
    [SerializeField] private string looking = "Look";

    private InputAction movementAction;
    private InputAction lookingAction;

    public Vector2 MovementInput { get; private set; }
    public Vector2 LookingInput { get; private set; }

    private void Awake()
    {
        InputActionMap mapRefernece = inputActionAsset.FindActionMap(actionMapName);
        movementAction = mapRefernece.FindAction(movement);
        lookingAction = mapRefernece.FindAction(looking);
    }

    private void SubscribeActions()
    {
        movementAction.performed += (input) => MovementInput = input.ReadValue<Vector2>();
        movementAction.canceled += (_) => MovementInput = Vector2.zero;

        lookingAction.performed += (input) => LookingInput = input.ReadValue<Vector2>();
        lookingAction.canceled += (_) => LookingInput = Vector2.zero;
    }

    private void UnsubscribeActions()
    {
        movementAction.performed -= (input) => MovementInput = input.ReadValue<Vector2>();
        movementAction.canceled -= (_) => MovementInput = Vector2.zero;

        lookingAction.performed -= (input) => LookingInput = input.ReadValue<Vector2>();
        lookingAction.canceled -= (_) => LookingInput = Vector2.zero;
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
