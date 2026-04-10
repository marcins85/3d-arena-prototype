using UnityEngine;

public class AttackState : IState
{
    private ActionStateMachine _sm;
    private ActionContext _ctx;

    public AttackState(ActionContext ctx, ActionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        SetAttack();
    }

    public void Exit()
    {
    }

    public void OnAnimationEvent(string evt)
    {
        if (evt == "OnAttackFinished")
        {
            _sm.SetState(_sm.AIdle);
        }
    }

    public void Update()
    {
        SetAttack();
    }

    private void SetAttack()
    {
        if (_ctx.Attack1Request)
        {
            _ctx.Animator.SetTrigger("Attack1");
            _ctx.Attack1Request = false;
        }

        if (_ctx.Attack2Request)
        {
            _ctx.Animator.SetTrigger("Attack2");
            _ctx.Attack2Request = false;
        }
    }
}
