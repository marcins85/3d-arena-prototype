using UnityEngine;

public class IdleState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;
    private ActionContext _actx;
    private ActionStateMachine _asm;

    public IdleState(LocomotionContext ctx, LocomotionStateMachine sm, ActionContext actx, ActionStateMachine asm)
    {
        _ctx = ctx;
        _sm = sm;
        _actx = actx;
        _asm = asm;
    }

    public void Enter()
    {
        _ctx.Animator.SetFloat("MoveSpeed", 0);
    }

    public void Exit() { }

    public void OnAnimationEvent(string evt)
    {
    }

    public void Update()
    {
        // TURN-IN-PLACE gdy stoi
        float delta = _ctx.Rotation.GetDeltaYaw();
        if (!_ctx.Rotation.IsTurning && _ctx.Rotation.JustStartedMovingForward && !_ctx.Rotation.IsMoving)
        {
            if (delta > _ctx.Config.moveTurnTreshold)
            {
                _sm.Turn.SetDirection(true);
                _sm.SetState(_sm.Turn);
                return;
            }
            else if (delta < -_ctx.Config.moveTurnTreshold)
            {
                _sm.Turn.SetDirection(false);
                _sm.SetState(_sm.Turn);
                return;
            }
        }

        if (_ctx.JumpRequest)
        {
            _sm.SetState(_sm.Jump);
            _ctx.JumpRequest = false;
            return;
        }
        if (_actx.Attack1Request || _actx.Attack2Request)
        {
            _asm.SetState(_asm.Attack);
             //if (_actx.Attack1Request) _actx.Attack1Request = false;
             //if (_actx.Attack2Request) _actx.Attack2Request = false;
            return;
        }

        if (_ctx.Velocity.sqrMagnitude > 0.01f)
        {
            _sm.SetState(_sm.Move);
        }
    }
}