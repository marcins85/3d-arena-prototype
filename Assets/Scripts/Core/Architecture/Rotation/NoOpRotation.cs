using UnityEngine;

public class NoOpRotation : IRotation
{
    public bool IsMoving { get; set; }
    public bool WantsToMove { get; set; }
    public bool JustStartedMovingForward { get; set; }
    public bool IsTurning { get; set; }

    public void SetLookInput(Vector2 input) { }
    public void SetMoveInput(Vector2 moveInput)
    {
        WantsToMove = moveInput != Vector2.zero;
    }
    public void HandleRotation() { }
    public float GetDeltaYaw() => 0f;
}