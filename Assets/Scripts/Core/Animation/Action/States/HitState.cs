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

        IDamage dmg = new NormalDamage(30);
        dmg = new PoisonDamage(dmg, 5);
        dmg = new DoubleDamage(dmg);

        int finalDamage = dmg.GetDamage();
        Debug.Log("Final damage: " + finalDamage);
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
