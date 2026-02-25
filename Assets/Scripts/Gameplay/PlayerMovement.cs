using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement
{
    private CharacterController _characterController;
    private Vector3 _worldDirection;
    private Vector3 _currentMovement;
    private Vector3 _airMovement;
    private Transform _transform;
    private float _walkSpeed = 5f;
    private float _sprintMultiplier = 1.5f;
    private Vector2 _moveInput;
    private bool _sprintTrigger;

    public PlayerMovement(CharacterController characterController, Transform transform)
    {
        _characterController = characterController;
        _transform = transform;
    }
    private Vector3 CalculateWorldDirection()
    {
        Vector3 l_inputDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        Vector3 l_worldDirection = _transform.TransformDirection(l_inputDirection);
        return l_worldDirection.normalized;
    }

    public void SetMoveInput(Vector2 input)
    {
        _moveInput = input;
        Debug.Log(_moveInput);
    }

    public void SetSprintTrigger(bool trigger)
    {
        _sprintTrigger = trigger;
    }

    public void HandleMovement()
    {
        if (_characterController == null) return;

        if (_characterController.isGrounded)
        {
            float l_moveSpeed = _walkSpeed * (_sprintTrigger ? _sprintMultiplier : 1);

            Vector3 l_worldDirection = CalculateWorldDirection();
            _currentMovement.x = l_worldDirection.x * l_moveSpeed;
            _currentMovement.z = l_worldDirection.z * l_moveSpeed;
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
