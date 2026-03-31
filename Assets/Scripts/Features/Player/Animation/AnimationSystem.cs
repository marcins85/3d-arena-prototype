
using UnityEngine;

public class AnimationSystem : IAnimationSystem
{
    // private readonly Animator _animator;
    private readonly LocomotionStateMachine _sm;
    private readonly LocomotionContext _ctx;

    public AnimationSystem(Animator animator)
    {
        // _animator = animator;
        _ctx = new LocomotionContext
        {
            Animator = animator
        };

        _sm = new LocomotionStateMachine();
        _sm.SetState(new IdleState(_ctx, _sm));
    }

    // public void PlayJump()
    // {
    //     _animator.SetTrigger("Jump");
    // }

    public void SetSprint(bool sprint)
    {
        _animator.SetBool("Sprint", sprint);
    }

    public void SetTurn(bool right)
    {
        _animator.SetTrigger(right ? "TurnRight" : "TurnLeft");
    }

    // public void UpdateMovement(Vector2 velocity)
    // {
    //     _animator.SetFloat("MoveSpeed", velocity.y);
    //     _animator.SetFloat("StrafeWalking", velocity.x);

    //     if (velocity.sqrMagnitude < 0.01f)
    //     {
    //         _animator.SetBool("Sprint", false);
    //     }

    //     if (velocity.y < 0.01f)
    //     {
    //         _animator.SetFloat("StrafeWalking", 0);
    //     }

    //     if (velocity.y == 0f)
    //     {
    //         _animator.SetFloat("JogStrafeWalking", velocity.x);
    //     }
    //     else
    //     {
    //         _animator.SetFloat("JogStrafeWalking", 0);
    //     }
    // }

    public void Update(Vector2 velocity, bool isGrounded)
    {
        _ctx.Velocity = velocity;
        _ctx.IsGrounded = isGrounded;

        _sm.Update();
    }
}
