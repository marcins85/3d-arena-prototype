using UnityEngine;
public class JumpState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;
    private IJump _jump;
    private bool _hasLeftGround;

    public JumpState(LocomotionContext ctx, LocomotionStateMachine sm, IJump jump)
    {
        _ctx = ctx;
        _sm = sm;
        _jump = jump;
        _hasLeftGround = false;
    }

    public void Enter()
    {
        _ctx.Animator.SetTrigger("Jump");
        _ctx.JumpRequest = false;

        _ctx.Animator.SetFloat("StrafeWalking", 0);
        _ctx.Animator.SetFloat("JogStrafeWalking", 0);
    }

    public void Exit() 
    {
        _hasLeftGround = false;
    }

    public void OnAnimationEvent(string evt)
    {
        if (evt == "OnJumpTakeOff")
        {
            _jump.SetJumpTrigger(true);
        }

        if (evt == "OnJumpLanding")
        {
            _jump.SetVerticalVelocity(0f);
        }

        if (evt == "OnJumpFinished")
        {
            _ctx.JumpRequest = false;
        }

    }

    public void Update()
    {
        if (!_hasLeftGround && !_ctx.IsGrounded)
        {
            _hasLeftGround = true;
        }

        if (_hasLeftGround && _ctx.IsGrounded && _ctx.VerticalVelocity <= 0)
        {
            _sm.SetState(_sm.Idle);
        }
    }
}