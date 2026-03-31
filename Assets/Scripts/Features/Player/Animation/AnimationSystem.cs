
using UnityEngine;

public class AnimationSystem : IAnimationSystem
{
    private readonly LocomotionStateMachine _sm;
    private readonly LocomotionContext _ctx;

    public AnimationSystem(Animator animator)
    {
        _ctx = new LocomotionContext
        {
            Animator = animator
        };

        _sm = new LocomotionStateMachine(_ctx);
        _sm.SetState(_sm.Idle);
    }

    public void PlayJump()
    {
        _sm.SetState(_sm.Jump);
    }

    public void SetSprint(bool sprint)
    {
        _ctx.Animator.SetBool("Sprint", sprint);
    }

    public void SetTurn(bool right)
    {
        _ctx.Animator.SetTrigger(right ? "TurnRight" : "TurnLeft");
    }

    public void Update(Vector2 velocity, bool isGrounded)
    {
        _ctx.Velocity = velocity;
        _ctx.IsGrounded = isGrounded;

        _sm.Update();
    }
}
