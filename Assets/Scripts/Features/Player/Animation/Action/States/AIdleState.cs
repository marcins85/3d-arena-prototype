public class AIdleState : IState
{
    private ActionStateMachine _sm;
    private ActionContext _ctx;

    public AIdleState(ActionContext ctx, ActionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter() { }
    public void Exit() { }
    public void Update() 
    {
        if (_ctx.BlockRequest)
        {
            _ctx.BlockRequest = false;
            _sm.SetState(_sm.Block);
            return;
        }
    }
    public void OnAnimationEvent(string evt) { }
}
