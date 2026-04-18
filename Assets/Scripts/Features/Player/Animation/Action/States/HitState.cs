using UnityEngine;

public class HitState : IState
{
    private ActionStateMachine _sm;
    private ActionContext _ctx;

    public HitState(ActionContext ctx, ActionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        _ctx.Animator.SetTrigger("Hit");
        _ctx.Movement.State = MovementState.Locked;
        _ctx.HitRequest = false;
    }

    public void Exit()
    {
        
    }

    public void OnAnimationEvent(string evt)
    {
        if (evt == "OnAnimationFinished")
        {
            _ctx.Movement.State = MovementState.Normal;
            _sm.SetState(_sm.AIdle);
        }
    }

    public void Update()
    {
    }
}
