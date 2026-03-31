public class JumpState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;

    public JumpState(LocomotionContext ctx, LocootionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        _ctx.Animator.SetTrigger("Jump");
    }

    public void Exit() { }

    public void Update()
    {
        if (_ctx.IsGrounded)
        {
            _sm.SetState(new IdleState(_ctx, _sm));
        }
    }
}