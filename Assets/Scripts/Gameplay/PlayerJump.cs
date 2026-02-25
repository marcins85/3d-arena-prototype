using UnityEngine;

public class PlayerJump
{
    private CharacterController _characterController;
    private PlayerMovement _movement;
    private float _jumpForce = 5f;
    private bool _canJump = true;
    private float _verticalVelocity = 0f;
    private float _gravityMultiplier = 1f;
    private bool _jumpTrigger;

    public PlayerJump(CharacterController characterController, PlayerMovement movement)
    {
        _characterController = characterController;
        _movement = movement;
    }

    public void SetJumpTrigger(bool trigger)
    {
        _jumpTrigger = trigger;
    }

    public void HandleJump()
    {
        if (_characterController == null || _movement == null) return;
        if (_characterController.isGrounded)
        {
            if (_jumpTrigger && _canJump)
            {
                _verticalVelocity = _jumpForce;
                _canJump = false;
            }

            if (!_jumpTrigger)
            {
                _canJump = true;
            }
        }

        _movement.SetCurrentMovement(new Vector3(
            _movement.GetCurrentMovement().x,
            _verticalVelocity,
            _movement.GetCurrentMovement().z
        ));

        SetGravity();
    }

    private void SetGravity()
    {
        if (_characterController.isGrounded)
        {
            if (_verticalVelocity < 0f)
            {
                _verticalVelocity = -2f;
            }
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * _gravityMultiplier * Time.deltaTime;
        }
    }
}
