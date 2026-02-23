using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement
{
    [SerializeField] private float moveSpeed = 5f;
    private PlayerInput _input;
    private CharacterController _characterController;
    private Vector3 _worldDirection;
    private Vector3 _currentMovement;
    private Vector3 _airMovement;
    private Transform _transform;
    
    public PlayerMovement(PlayerInput input, CharacterController characterController, Transform transform)
    {
        _input = input;
        _characterController = characterController;
        _transform = transform;
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y);
        Vector3 worldDirection = _transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    public void HandleMovement()
    {
        if (_characterController == null && _input == null) return;
            
        if (_characterController.isGrounded)
        {
            Vector3 worldDirection = CalculateWorldDirection();
            _currentMovement.x = worldDirection.x * moveSpeed;
            _currentMovement.z = worldDirection.z * moveSpeed;
            _airMovement = new Vector3(_currentMovement.x, 0f, _currentMovement.z);
        }
        else
        {
            _currentMovement.x = _airMovement.x;
            _currentMovement.z = _airMovement.z;
        }

        _characterController.Move(_currentMovement * Time.deltaTime);
    }

    public Vector3 GetAirMovement()
    {
        return _airMovement; 
    }
    public void SetAirMovement(Vector3 value)
    {
        _airMovement = value; 
    }

    public Vector3 GetCurrentMovement()
    {
        return _currentMovement; 
    }
    public void SetCurrentMovement(Vector3 value)
    {
        _currentMovement = value;
    }
}
