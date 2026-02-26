public interface IJump
{
    public float GetVerticalVelocity();
    public void SetVerticalVelocity(float value);
    public void SetJumpTrigger(bool trigger);
    public void HandleJump(bool isGrounded);
}
