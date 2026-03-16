using UnityEngine;

public interface IJumpHandler
{
    public void OnJumpStarted();
    public void OnJumpFinished();
}

public class PlayerJump : IJump, IJumpHandler
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
        Debug.Log("canjump " + CanJump + ", trigger " + _jumpTrigger);
        if (isGrounded)
        {
            if (_jumpTrigger && CanJump)
            {
                _verticalVelocity = _config.jumpForce;
            }
        }
    }

    public void OnJumpStarted()
    {
        CanJump = false;
    }

    public void OnJumpFinished()
    {
        CanJump = true;
    }
}
