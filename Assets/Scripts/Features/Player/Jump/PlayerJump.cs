using UnityEngine;

public class PlayerJump : IJump
{
    private PlayerConfigSO _config;
    private float _verticalVelocity = 0f;
    private bool _jumpTrigger;
    public bool CanJump { get; set; }

    public PlayerJump(PlayerConfigSO confg)
    {
        _config = confg;
        CanJump = true;
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
            if (_verticalVelocity < 0f)
                _verticalVelocity = -2f;

            if (_jumpTrigger)
            {
                _verticalVelocity = _config.jumpForce;
                _jumpTrigger = false;
                CanJump = false;
            }
            else
            {
                CanJump = true;
            }
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * _config.gravityMultiplier * Time.deltaTime;
        }
    }
}
