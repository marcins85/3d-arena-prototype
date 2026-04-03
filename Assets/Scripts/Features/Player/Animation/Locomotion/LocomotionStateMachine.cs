public class LocomotionStateMachine : StateMachine
{
    public IState Idle;
    public IState Move;
    public IState Jump;
    public IState JumpStart;

    public LocomotionStateMachine(LocomotionContext ctx, IJump jump)
    {
        Idle = new IdleState(ctx, this);
        Move = new MoveState(ctx, this);
        Jump = new JumpState(ctx, this, jump);
        JumpStart = new JumpStartState(ctx, this, jump);
    }
}