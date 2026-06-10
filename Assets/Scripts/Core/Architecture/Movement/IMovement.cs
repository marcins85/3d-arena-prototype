using UnityEngine;

public interface IMovement
{
    public float CurrentVerticalVelocity { get; }
    public MovementState State { get; set; }
    public bool CanMove { get; }
    public void SetMoveInput(Vector2 input);
    public void SetSprintTrigger(bool trigger);
    public bool IsGroundedRaycast();
    public void HandleMovement(float verticalVelocity);
}
