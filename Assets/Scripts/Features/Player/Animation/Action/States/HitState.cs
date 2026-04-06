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
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void OnAnimationEvent(string evt)
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}
