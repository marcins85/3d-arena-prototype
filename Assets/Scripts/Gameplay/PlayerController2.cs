using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Transform _camTarget;
    private PlayerMovement _movement;
    private PlayerRotation _rotation;
    private PlayerJump _jump;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _movement = new PlayerMovement(_input, _characterController, transform);
        _rotation = new PlayerRotation(_input, _camTarget, transform);
        _jump = new PlayerJump(_input, _characterController, _movement);
    }

    void Update()
    {
        _movement.HandleMovement();
        _rotation.HandleRotation();
        _jump.HandleJump();
    }
}
