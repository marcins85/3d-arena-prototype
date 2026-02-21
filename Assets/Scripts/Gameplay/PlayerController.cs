using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parms")]
    [SerializeField] private float walkSpeed = 3.0f;

    [Header("Looking Params")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float upDownLookRange = 80f;

    [Header("Jumping Params")]
    [SerializeField] private float jumpForce = 50f;
    [SerializeField] private float gravityMultiplier = 1f;

    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform camTarget;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    private Vector3 currentMovement;
    private Vector3 airMovement;
    private float verticalRotation;
    private float verticalVelocity = 0f;
    private bool canJump = true;
    private float currentSpeed => walkSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MoveInput.x, 0f, playerInputHandler.MoveInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    private void HandleMovement()
    {
        if (characterController.isGrounded)
        {
            Vector3 worldDirection = CalculateWorldDirection();
            currentMovement.x = worldDirection.x * currentSpeed;
            currentMovement.z = worldDirection.z * currentSpeed;

            airMovement = new Vector3(currentMovement.x, 0f, currentMovement.z);
        }
        else
        {
            currentMovement.x = airMovement.x;
            currentMovement.z = airMovement.z;
        }
        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }

    private void VerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(verticalRotation - rotationAmount, -upDownLookRange, upDownLookRange);
        if (camTarget != null)
            camTarget.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void HandleRotation()
    {
        float mouseXRotation = playerInputHandler.LookInput.x * mouseSensitivity;
        float mouseYRotation = playerInputHandler.LookInput.y * mouseSensitivity;

        HorizontalRotation(mouseXRotation);
        VerticalRotation(mouseYRotation);
    }

    private void HandleJump()
    {
        if (characterController.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f;
            }

            if (playerInputHandler.JumpTrigger && canJump)
            {
                verticalVelocity = jumpForce;
                airMovement = new Vector3(currentMovement.x, 0f, currentMovement.z);
                canJump = false;
            }

            if (!playerInputHandler.JumpTrigger)
            {
                canJump = true;
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        currentMovement.y = verticalVelocity;
    }

}
