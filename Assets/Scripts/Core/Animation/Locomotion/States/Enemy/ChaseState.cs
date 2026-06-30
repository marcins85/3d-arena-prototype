using UnityEngine;

public class ChaseState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;
    private ActionContext _actx;
    private ActionStateMachine _asm;

    public ChaseState(LocomotionContext ctx, LocomotionStateMachine sm, ActionContext actx, ActionStateMachine asm)
    {
        _ctx = ctx;
        _sm = sm;
        _actx = actx;
        _asm = asm;
    }

    public void Enter()
    {
        Debug.Log("ChaseState Enter");
        _sm.SetState(_sm.Move);
    }

    public void Exit()
    {
    }

    public void OnAnimationEvent(string evt)
    {
    }

    public void Update()
    {
        if (!_ctx.WalkPointSet && _ctx.TryAttack)
        {
            Debug.Log("ChaseState Update set attack");
            _asm.SetState(_asm.Attack);
        }
    }
}