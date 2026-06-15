using UnityEngine;

public class AttackState : IState
{
    private readonly ActionStateMachine _sm;
    private readonly ActionContext _ctx;

    public AttackState(ActionContext ctx, ActionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void Enter()
    {
        _ctx.DefenceWindowOpen = true;
        _ctx.BlockHeld = false;
        _ctx.BlockRequest = false;

        if (_ctx.Attack2Request)
        {
            _ctx.Animator.SetTrigger("Attack2");
            _ctx.Attack2Request = false;
            return;
        }

        StartCombo();
    }

    public void Exit()
    {
        _ctx.ComboStep = 0;
        _ctx.QueuedAttack = false;
    }

    public void Update()
    {
        if (_ctx.DefenceWindowOpen && _ctx.BlockRequest)
        {
            _ctx.DefenceWindowOpen = false;
            _ctx.BlockRequest = false;

            _sm.SetState(_sm.Block);
            return;
        }
    }

    public void OnAnimationEvent(string evt)
    {
        if (evt == "BlockWindowClosed")
        {
            _ctx.DefenceWindowOpen = false;
        }

        if (evt == "ComboWindowOpen")
        {
            if (_ctx.Attack1Request)
            {
                _ctx.QueuedAttack = true;
                _ctx.Attack1Request = false;
                _ctx.DefenceWindowOpen = true;

            }
        }

        if (evt == "ComboTransition")
        {
            if (_ctx.QueuedAttack)
            {
                _ctx.QueuedAttack = false;
                ContinueCombo();
            }
        }

        if (evt == "OnAttackFinished")
        {
            _sm.SetState(_sm.AIdle);
        }
    }

    private void StartCombo()
    {
        _ctx.ComboStep = 1;
        _ctx.Animator.SetInteger("ComboIndex", _ctx.ComboStep);
        _ctx.Animator.SetTrigger("Attack1");

        _ctx.Attack1Request = false;
    }

    private void ContinueCombo()
    {
        _ctx.ComboStep++;

        if (_ctx.ComboStep > 3)
            _ctx.ComboStep = 0;

        _ctx.Animator.SetInteger("ComboIndex", _ctx.ComboStep);
        _ctx.Animator.SetTrigger("Attack1");
    }
}