using UnityEngine;

public interface IMovement
{
    public float CurrentVerticalVelocity { get; }
    public bool CanMove { get; set; }
    public void SetMoveInput(Vector2 input);
    public void SetSprintTrigger(bool trigger);
    public void HandleMovement(float verticalVelocity);
}
