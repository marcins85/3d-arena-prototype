using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parms")]
    [SerializeField] private float walkSpeed = 3.0f;

    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    private Vector3 currentMovement;
    private float currentSpeed => walkSpeed;

    void Start()
    {

    }

    void Update()
    {
        HandleMovement();
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Debug.Log(inputDirection.x);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection;
    }

    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * currentSpeed;
        currentMovement.z = worldDirection.z * currentSpeed;
        Debug.Log(worldDirection.x);
        characterController.Move(currentMovement * Time.deltaTime);
    }
}
