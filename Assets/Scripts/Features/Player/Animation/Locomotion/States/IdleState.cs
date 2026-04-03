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

    public void OnAnimationEvent(string evt)
    {
    }

    public void Update()
    {
        if (_ctx.JumpRequest)
        {
            _sm.SetState(_sm.Jump);
            _ctx.JumpRequest = false;
            return;
        }

        if (_ctx.Velocity.sqrMagnitude > 0.01f)
        {
            _sm.SetState(_sm.Move);
        }
    }
}