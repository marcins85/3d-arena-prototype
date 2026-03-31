using UnityEngine;

public interface IAnimationSystem
{
    //public void UpdateMovement(Vector2 velocity);
    public void Update(Vector2 velocity, bool isGrounded);
    public void SetSprint(bool sprint);
    public void PlayJump();
    public void SetTurn(bool right);
}
