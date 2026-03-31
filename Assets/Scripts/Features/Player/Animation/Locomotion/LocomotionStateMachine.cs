public class LocomotionStateMachine : StateMachine
{
    public IState Idle;
    public IState Move;
    public IState Jump;

    public LocomotionStateMachine(LocomotionContext ctx)
    {
        Idle = new IdleState(ctx, this);
        Move = new MoveState(ctx, this);
        Jump = new JumpState(ctx, this);
    }
}