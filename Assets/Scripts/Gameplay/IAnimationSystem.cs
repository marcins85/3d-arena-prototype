using UnityEngine;

public interface IAnimationSystem
{
    public void UpdateMovement(Vector2 velocity);
    public void SetSprint(bool sprint);
    public void PlayJump();
    public void SetTurn(bool right);
}
