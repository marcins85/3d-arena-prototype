
using UnityEngine;

public class AnimationSystem : IAnimationSystem
{
    private readonly LocomotionStateMachine _locomotion;
    private readonly LocomotionContext _locomotionCtx;
    private readonly ActionStateMachine _action;
    private readonly ActionContext _actionCtx;

    public AnimationSystem(PlayerConfigSO config, IMovement movement, Animator animator, IJump jump, IRotation rotation)
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
            Animator = animator
        };

        _action = new ActionStateMachine(_actionCtx);
        _locomotion = new LocomotionStateMachine(_locomotionCtx, _actionCtx, _action);
        _locomotion.SetState(_locomotion.Idle);

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

    public void Update(Vector2 velocity, bool isGrounded, float verticalVelocity, bool jumpRequest)
    {
        _locomotionCtx.Velocity = velocity;
        _locomotionCtx.IsGrounded = isGrounded;
        _locomotionCtx.VerticalVelocity = verticalVelocity;
        _locomotionCtx.JumpRequest = jumpRequest;

        _locomotion.Update();
        _action.Update();
    }
}
