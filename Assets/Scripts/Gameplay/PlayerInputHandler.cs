using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private string actionMapName = "Main";
    [SerializeField] private string movement = "Move";

    private InputAction movementAction;

    public Vector2 MovementInput { get; private set; }

    private void Awake()
    {
        InputActionMap mapRefernece = inputActionAsset.FindActionMap(actionMapName);
        movementAction = mapRefernece.FindAction(movement);
    }

    private void SubscribeActions()
    {
        movementAction.performed += (input) => MovementInput = input.ReadValue<Vector2>();
        movementAction.canceled += (_) => MovementInput = Vector2.zero;

    }

    private void UnsubscribeActions()
    {
        movementAction.performed -= (input) => MovementInput = input.ReadValue<Vector2>();
        movementAction.canceled -= (_) => MovementInput = Vector2.zero;
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
