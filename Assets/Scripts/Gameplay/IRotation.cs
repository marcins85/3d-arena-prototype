using UnityEngine;

public interface IRotation
{
    public bool IsTurning { get; set; }
    public void SetLookInput(Vector2 input);
    public void HandleRotation();
}
