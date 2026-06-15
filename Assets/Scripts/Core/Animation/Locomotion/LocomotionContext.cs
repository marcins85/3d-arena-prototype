using UnityEngine;
public class LocomotionContext
{
    public PlayerConfigSO Config;
    public IMovement Movement;
    public IRotation Rotation;
    public IJump Jump;
    public Animator Animator;
    public Vector2 Velocity;
    public float VerticalVelocity;
    public bool IsGrounded;
    public bool JumpRequest;
}