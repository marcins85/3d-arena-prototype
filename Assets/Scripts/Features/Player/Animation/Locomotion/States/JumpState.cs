using NUnit.Framework;

public class JumpState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;
    private bool _hasLeftGround;

    public JumpState(LocomotionContext ctx, LocomotionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
        _hasLeftGround = false;
    }

    public void Enter()
    {
        _ctx.Animator.SetTrigger("Jump");

        _ctx.Animator.SetFloat("StrafeWalking", 0);
        _ctx.Animator.SetFloat("JogStrafeWalking", 0);
    }

    public void Exit() 
    {
        _hasLeftGround = false;
    }

    public void Update()
    {
        if (!_hasLeftGround && !_ctx.IsGrounded)
        {
            _hasLeftGround = true;
        }

        if (_hasLeftGround && _ctx.IsGrounded)
        {
            _sm.SetState(_sm.Idle);
        }
    }
}