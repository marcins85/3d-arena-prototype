using System;
using UnityEngine;

public class JumpStartState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;
    private IJump _jump;

    public JumpStartState(LocomotionContext ctx, LocomotionStateMachine sm, IJump jump)
    {
        _ctx = ctx;
        _sm = sm;
        _jump = jump;
    }

    public void Enter()
    {
        //_ctx.Animator.SetTrigger("Jump");
        //_sm.SetState(_sm.Jump);
    }

    public void Exit()
    {
    }
    public void OnAnimationEvent(string evt)
    {
        
    }

    public void Update()
    {
        //_timer += Time.deltaTime;

        //if (_timer > 0.1f)
        //{
        //    _jump.SetJumpTrigger(true);
        //    _sm.SetState(_sm.Jump);
        //}
    }
}
