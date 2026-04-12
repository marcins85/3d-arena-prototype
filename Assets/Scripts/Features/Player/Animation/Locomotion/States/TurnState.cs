using UnityEngine;

public class TurnState : IState
{
    private LocomotionContext _ctx;
    private LocomotionStateMachine _sm;
    private bool _right;

    public TurnState(LocomotionContext ctx, LocomotionStateMachine sm)
    {
        _ctx = ctx;
        _sm = sm;
    }

    public void SetDirection(bool right)
    {
        _right = right;
    }

    public void Enter()
    {
        //_ctx.Movement.CanMove = false;
        _ctx.Movement.State = MovementState.Locked;
        _ctx.Rotation.IsTurning = true;
        _ctx.Animator.SetTrigger(_right ? "TurnRight" : "TurnLeft");
    }

    public void Exit()
    {
    }

    public void OnAnimationEvent(string evt)
    {
        if (evt == "OnTurnLeftFinished" || evt == "OnTurnRightFinished")
        {
            _ctx.Rotation.IsTurning = false;
            _ctx.Rotation.IsMoving = _ctx.Rotation.WantsToMove;
            //_ctx.Movement.CanMove = true;
            _ctx.Movement.State = MovementState.Normal;

            if (_ctx.Rotation.IsMoving)
            {
                _sm.SetState(_sm.Move);
            }
            else
            {
                _sm.SetState(_sm.Idle);
            }
        }
    }

    public void Update()
    {
        
    }
}
