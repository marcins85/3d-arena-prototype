using UnityEngine;

public class BlockState : IState
{
    private ActionStateMachine _sm;
    private ActionContext _ctx;

    public BlockState(ActionContext ctx, ActionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        _ctx.Animator.SetBool("IsBlocking", true);
        _ctx.BlockRequest = false;

    }

    public void Exit()
    {
        _ctx.Animator.SetBool("IsBlocking", false);
        _ctx.BlockHeld = false;
    }

    public void OnAnimationEvent(string evt)
    {
        
    }

    public void Update()
    {
        if (!_ctx.BlockHeld)
        {
            _sm.SetState(_sm.AIdle);
        }
    }
}
