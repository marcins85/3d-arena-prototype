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
    }

    public void Exit()
    {
    }

    public void OnAnimationEvent(string evt)
    {
    }

    public void Update()
    {
        if (!_ctx.WalkPointSet && !_ctx.TryAttack)
        {
            _sm.SetState(_sm.Chase);
        }
    }
}