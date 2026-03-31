public class MoveState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;

    public MoveState(LocomotionContext ctx, LocootionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter() { }

    public void Exit() { }

    public void Update()
    {
        _ctx.Animator.SetFloat("MoveSpeed", _ctx.Velocity.y);
        _ctx.Animator.SetFloat("StrafeWalking", _ctx.Velocity.x);

        if (_ctx.Velocity.y < 0.01f)
        {
            _ctx.Animator.SetFloat("StrafeWalking", 0);
        }

        if (_ctx.Velocity.y == 0f)
        {
            _ctx.Animator.SetFloat("JogStrafeWalking", _ctx.Velocity.x);
        }
        else
        {
            _ctx.Animator.SetFloat("JogStrafeWalking", 0);
        }

        if (!_ctx.IsGrounded)
        {
            _sm.SetState(new JumpState(_ctx, _sm));
            return;
        }

        if (_ctx.Velocity.sqrMagnitude < 0.01f)
        {
            _ctx.Animator.SetBool("Sprint", false);
            _sm.SetState(new IdleState(_ctx, _sm));
        }
    }
}