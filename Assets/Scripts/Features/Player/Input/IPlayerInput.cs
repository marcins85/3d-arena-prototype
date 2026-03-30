using System;
using UnityEngine;

public interface IPlayerInput
{
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<bool> OnSprint;
    public event Action<bool> OnJump;

    public void Enable();
    public void Disable();
}
