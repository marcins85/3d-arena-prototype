using UnityEngine;

public class PatrolState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;

    public PatrolState(LocomotionContext ctx, LocomotionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        // _sm.SetState(_sm.Move);
        Debug.Log("PatrolState Enter");
    }

    public void Exit()
    {
    }

    public void OnAnimationEvent(string evt)
    {
    }

    public void Update()
    {
        Debug.Log(_ctx.WalkPointSet);
        Debug.Log(_ctx.TryAttack);
        if (!_ctx.WalkPointSet && !_ctx.TryAttack)
        {
            Debug.Log("PatrolState Update set chase");
            _sm.SetState(_sm.Chase);
        }
    }
}