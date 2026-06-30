public class LocomotionStateMachine : StateMachine
{
    public IState Idle;
    public IState Move;
    public IState Jump;
    public TurnState Turn;
    public IState Patrol;
    public IState Chase;

    public LocomotionStateMachine(LocomotionContext ctx, ActionContext actx, ActionStateMachine asm)
    {
        Idle = new IdleState(ctx, this, actx, asm);
        Move = new MoveState(ctx, this);
        Jump = new JumpState(ctx, this);
        Turn = new TurnState(ctx, this);
        Patrol = new PatrolState(ctx, this);
        Chase = new ChaseState(ctx, this, actx, asm);
    }
}