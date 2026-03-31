public class IdleState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;

    public IdleState(LocomotionContext ctx, LocomotionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        _ctx.Animator.SetFloat("MoveSpeed", 0);
    }

    public void Exit() { }

    public void Update()
    {
        //if (!_ctx.IsGrounded)
        //{
        //    _sm.SetState(new JumpState(_ctx, _sm));
        //    return;
        //}

        if (_ctx.Velocity.sqrMagnitude > 0.01f)
        {
            _sm.SetState(_sm.Move);
        }
    }
}