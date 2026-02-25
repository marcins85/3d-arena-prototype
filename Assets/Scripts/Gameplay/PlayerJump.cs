using UnityEngine;

public class PlayerJump
{
    private float _jumpForce = 5f;
    private bool _canJump = true;
    private float _verticalVelocity = 0f;
    private bool _jumpTrigger;

    public PlayerJump()
    {
    }

    public float GetVerticalVelocity()
    {
        return _verticalVelocity; 
    }

    public void SetVerticalVelocity(float value)
    {
        _verticalVelocity = value;
    }

    public void SetJumpTrigger(bool trigger)
    {
        _jumpTrigger = trigger;
    }

    public void HandleJump(bool isGrounded)
    {
        if (isGrounded)
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
    }
}
