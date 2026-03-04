public interface IJump
{
    public float GetVerticalVelocity();
    public void SetVerticalVelocity(float p_value);
    public void SetJumpTrigger(bool p_trigger);
    public void HandleJump(bool p_isGrounded);
}
