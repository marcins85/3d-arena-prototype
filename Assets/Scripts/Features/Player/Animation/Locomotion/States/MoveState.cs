public class MoveState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;

    public MoveState(LocomotionContext ctx, LocomotionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter() { }

    public void Exit() { }

    public void OnAnimationEvent(string evt)
    {
    }

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

        if (_ctx.JumpRequest)
        {
            _sm.SetState(_sm.Jump);
            _ctx.JumpRequest = false;
            return;
        }

        if (_ctx.Velocity.sqrMagnitude < 0.01f)
        {
            _ctx.Animator.SetBool("Sprint", false);
            _sm.SetState(_sm.Idle);
        }
    }
}