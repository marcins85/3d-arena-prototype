using System;
using UnityEngine;

public interface IRotation
{
    public bool IsTurning { get; set; }
    public bool IsMoving { get; set; }
    public bool WantsToMove { get; set; }
    public bool JustStartedMovingForward { get; set; }
    public float GetDeltaYaw();
    public void SetLookInput(Vector2 input);
    public void SetMoveInput(Vector2 input);
    public void HandleRotation();
}
