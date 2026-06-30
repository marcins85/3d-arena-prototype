using UnityEngine;

public class EnemyRotation : IRotation
{
    public bool IsTurning { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool IsMoving { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool WantsToMove { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool JustStartedMovingForward { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public float GetDeltaYaw()
    {
        throw new System.NotImplementedException();
    }

    public void HandleRotation()
    {
        throw new System.NotImplementedException();
    }

    public void SetLookInput(Vector2 input)
    {
        throw new System.NotImplementedException();
    }

    public void SetMoveInput(Vector2 input)
    {
        throw new System.NotImplementedException();
    }
}
