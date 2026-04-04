using UnityEngine;

public interface IAnimationSystem
{
    public void Update(Vector2 velocity, bool isGrounded, float verticalVelocity, bool jumpRequest);
    public void SetSprint(bool sprint);
    public void OnJumpTakeOff();
    public void OnJumpLanding();
    public void OnJumpFinished();
    public void OnTurnLeftFinished();
    public void OnTurnRightFinished();
}
