
using UnityEngine;

public class AnimationSystem : IAnimationSystem
{
    private readonly LocomotionStateMachine _locomotion;
    private readonly LocomotionContext _locomotionCtx;
    private readonly ActionStateMachine _action;
    private readonly ActionContext _actionCtx;

    // for player
    public AnimationSystem(IEntityConfig config, IMovement movement, Animator animator, IJump jump, IRotation rotation)
    {
        _locomotionCtx = new LocomotionContext
        {
            Config = config,
            Movement = movement,
            Rotation = rotation,
            Jump = jump,
            Animator = animator
        };

        _actionCtx = new ActionContext
        {
            Animator = animator,
            Movement = movement,
        };

        _action = new ActionStateMachine(_actionCtx);
        _locomotion = new LocomotionStateMachine(_locomotionCtx, _actionCtx, _action);
        _locomotion.SetState(_locomotion.Idle);
    }

    // for enemy
    public AnimationSystem(EnemyConfigSO config, IMovement movement, Animator animator, IJump jump, IRotation rotation)
    {
        _locomotionCtx = new LocomotionContext
        {
            Config = config,
            Movement = movement,
            Rotation = rotation,
            Jump = jump,
            Animator = animator
        };

        _actionCtx = new ActionContext
        {
            Animator = animator,
            Movement = movement,
        };

        _action = new ActionStateMachine(_actionCtx);
        _locomotion = new LocomotionStateMachine(_locomotionCtx, _actionCtx, _action);
        _locomotion.SetState(_locomotion.Patrol);
        Debug.Log("Enemy AnimationSystem controller");
    }

    public void OnJumpTakeOff()
    {
        _locomotion.HandleAnimationEvent("OnJumpTakeOff");
    }

    public void OnJumpLanding()
    {
        _locomotion.HandleAnimationEvent("OnJumpLanding");
    }

    public void OnJumpFinished()
    {
        _locomotion.HandleAnimationEvent("OnJumpFinished");
    }

    public void OnTurnLeftFinished()
    {
        _locomotion.HandleAnimationEvent("OnTurnLeftFinished");
    }

    public void OnTurnRightFinished()
    {
        _locomotion.HandleAnimationEvent("OnTurnRightFinished");
    }

    public void SetSprint(bool sprint)
    {
        _locomotionCtx.Animator.SetBool("Sprint", sprint);
    }

    public void OnAttackFinished()
    {
        _action.HandleAnimationEvent("OnAttackFinished");
    }

    public void OnAnimationFinished()
    {
        _action.HandleAnimationEvent("OnAnimationFinished");
    }

    public void ComboWindowOpen()
    {
        _action.HandleAnimationEvent("ComboWindowOpen");
    }

    public void ComboTransition()
    {
        _action.HandleAnimationEvent("ComboTransition");
    }

    public void RequestAttack1()
    {
        _actionCtx.Attack1Request = true;
    }

    public void RequestAttack2()
    {
        _actionCtx.Attack2Request = true;
    }

    public void RequestHit()
    {
        _actionCtx.HitRequest = true;
    }

    public void RequestBlock()
    {
        _actionCtx.BlockRequest = true;
    }

    public void SetBlockHeld(bool held)
    {
        _actionCtx.BlockHeld = held;
    }

    public void BlockWindowClosed()
    {
        _action.HandleAnimationEvent("BlockWindowClosed");
    }

    // enemy
    public void SetWalkPointSet(bool value)
    {
        _locomotionCtx.WalkPointSet = value;
    }

    // enemy
    public void SetTryAttack(bool value)
    {
        _locomotionCtx.TryAttack = value;
    }

    public void Update(Vector2 velocity, bool isGrounded, float verticalVelocity, bool jumpRequest)
    {
        if (_actionCtx.HitRequest)
        {
            _actionCtx.HitRequest = false;
            _action.SetState(_action.Hit);
            return;
        }

        _locomotionCtx.Velocity = velocity;
        _locomotionCtx.IsGrounded = isGrounded;
        _locomotionCtx.VerticalVelocity = verticalVelocity;
        _locomotionCtx.JumpRequest = jumpRequest;

        _locomotion.Update();
        _action.Update();
    }
}
