using UnityEngine;

public class PlayerJump : IJump
{
    private PlayerConfigSO _config;
    private bool _canJump = true;
    private float _verticalVelocity = 0f;
    private bool _jumpTrigger;

    public PlayerJump(PlayerConfigSO confg)
    {
        _config = confg;
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
                _verticalVelocity = _config.jumpForce;
                _canJump = false;
            }

            if (!_jumpTrigger)
            {
                _canJump = true;
            }
        }
    }
}
