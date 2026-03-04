using UnityEngine;

public interface IMovement
{
    public float CurrentVerticalVelocity { get; }
    public bool CanMove { get; set; }
    public void SetMoveInput(Vector2 p_input);
    public void SetSprintTrigger(bool p_trigger);
    public void HandleMovement(float p_verticalVelocity);
}
