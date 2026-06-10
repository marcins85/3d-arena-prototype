using System;
using UnityEngine;

public class EnemyMovement : IMovement
{
    public float CurrentVerticalVelocity => throw new NotImplementedException();

    public MovementState State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool CanMove => throw new NotImplementedException();

    public void HandleMovement(float verticalVelocity)
    {
        throw new NotImplementedException();
    }

    public bool IsGroundedRaycast()
    {
        throw new NotImplementedException();
    }

    public void SetMoveInput(Vector2 input)
    {
        throw new NotImplementedException();
    }

    public void SetSprintTrigger(bool trigger)
    {
        throw new NotImplementedException();
    }
}
