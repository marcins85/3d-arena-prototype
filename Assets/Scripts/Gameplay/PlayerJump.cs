using UnityEngine;

public class PlayerJump : IJump
{
    private PlayerConfigSO m_config;
    private bool m_canJump = true;
    private float m_verticalVelocity = 0f;
    private bool m_jumpTrigger;

    public PlayerJump(PlayerConfigSO p_confg)
    {
        m_config = p_confg;
    }

    public float GetVerticalVelocity()
    {
        return m_verticalVelocity; 
    }

    public void SetVerticalVelocity(float p_value)
    {
        m_verticalVelocity = p_value;
    }

    public void SetJumpTrigger(bool p_trigger)
    {
        m_jumpTrigger = p_trigger;
    }

    public void HandleJump(bool p_isGrounded)
    {
        if (p_isGrounded)
        {
            if (m_jumpTrigger && m_canJump)
            {
                m_verticalVelocity = m_config.jumpForce;
                m_canJump = false;
            }

            if (!m_jumpTrigger)
            {
                m_canJump = true;
            }
        }
    }
}
