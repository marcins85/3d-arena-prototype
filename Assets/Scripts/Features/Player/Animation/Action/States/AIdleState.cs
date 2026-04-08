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
    public void Update() { }
    public void OnAnimationEvent(string evt) { }
}
