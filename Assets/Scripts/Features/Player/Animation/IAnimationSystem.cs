using UnityEngine;

public interface IAnimationSystem
{
    public void Update(Vector2 velocity, bool isGrounded, float verticalVelocity, bool jumpRequest);
    public void SetSprint(bool sprint);
    public void RequestAttack1();
    public void RequestAttack2();
    public void RequestBlock();
    public void SetBlockHeld(bool held);
    public void OnJumpTakeOff();
    public void OnJumpLanding();
    public void OnJumpFinished();
    public void OnTurnLeftFinished();
    public void OnTurnRightFinished();
    public void ComboWindowOpen();
    public void ComboTransition();
    public void OnAttackFinished();
    public void BlockWindowClosed();
}
