
using UnityEngine;

public class AnimationSystem : IAnimationSystem
{
    private readonly LocomotionStateMachine _sm;
    private readonly LocomotionContext _ctx;

    public AnimationSystem(PlayerConfigSO config, IMovement movement, Animator animator, IJump jump, IRotation rotation)
    {
        _ctx = new LocomotionContext
        {
            Config = config,
            Movement = movement,
            Rotation = rotation,
            Jump = jump,
            Animator = animator
        };

        _sm = new LocomotionStateMachine(_ctx);
        _sm.SetState(_sm.Idle);
    }

    public void OnJumpTakeOff()
    {
        _sm.HandleAnimationEvent("OnJumpTakeOff");
    }

    public void OnJumpLanding()
    {
        _sm.HandleAnimationEvent("OnJumpLanding");
    }

    public void OnJumpFinished()
    {
        _sm.HandleAnimationEvent("OnJumpFinished");
    }

    public void OnTurnLeftFinished()
    {
        _sm.HandleAnimationEvent("OnTurnLeftFinished");
    }

    public void OnTurnRightFinished()
    {
        _sm.HandleAnimationEvent("OnTurnRightFinished");
    }

    public void SetSprint(bool sprint)
    {
        _ctx.Animator.SetBool("Sprint", sprint);
    }

    //public void SetTurn(bool right)
    //{
    //    _ctx.Animator.SetTrigger(right ? "TurnRight" : "TurnLeft");
    //}

    public void Update(Vector2 velocity, bool isGrounded, float verticalVelocity, bool jumpRequest)
    {
        _ctx.Velocity = velocity;
        _ctx.IsGrounded = isGrounded;
        _ctx.VerticalVelocity = verticalVelocity;
        _ctx.JumpRequest = jumpRequest;

        _sm.Update();
    }
}
