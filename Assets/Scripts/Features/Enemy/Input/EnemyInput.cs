using System;
using UnityEngine;

public class EnemyInput : IEnemyInput
{
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<bool> OnSprint;
    public event Action<bool> OnJump;
    public event Action<bool> OnAttack1;
    public event Action<bool> OnAttack2;
    public event Action<bool> OnBlock;

    public void Disable()
    {
        throw new NotImplementedException();
    }

    public void Enable()
    {
        throw new NotImplementedException();
    }
}
