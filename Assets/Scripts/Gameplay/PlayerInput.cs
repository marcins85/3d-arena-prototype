using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : IPlayerInput
{
    private readonly string _mapName = "Main";
    private readonly string _moveName = "Move";
    private readonly string _lookName = "Look";
    private readonly string _sprintName = "Sprint";
    private readonly string _jumpName = "Jump";

    private readonly InputAction _moveAction;
    private readonly InputAction _lookAction;
    private readonly InputAction _sprintAction;
    private readonly InputAction _jumpAction;

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<bool> OnSprint;
    public event Action<bool> OnJump;

    public PlayerInput(InputActionAsset asset)
    {
        var l_map = asset.FindActionMap(_mapName);
        _moveAction = l_map.FindAction(_moveName);
        _lookAction = l_map.FindAction(_lookName);
        _sprintAction = l_map.FindAction(_sprintName);
        _jumpAction = l_map.FindAction(_jumpName);

        _moveAction.performed += MovePerformed;
        _moveAction.canceled += MoveCanceled;

        _lookAction.performed += LookPerformed;
        _lookAction.canceled += LookCanceled;

        _sprintAction.performed += SprintPerformed;
        _sprintAction.canceled += SprintCanceled;

        _jumpAction.performed += JumpPerformed;
        _jumpAction.canceled += JumpCanceled;
    }

    public void Enable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
        _sprintAction.Enable();
        _jumpAction.Enable();
    }
    public void Disable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
        _sprintAction.Disable();
        _jumpAction.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }
    private void MoveCanceled(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(Vector2.zero);
    }

    private void LookPerformed(InputAction.CallbackContext ctx)
    {
        OnLook?.Invoke(ctx.ReadValue<Vector2>());
    }
    private void LookCanceled(InputAction.CallbackContext ctx)
    {
        OnLook?.Invoke(Vector2.zero);
    }

    private void SprintPerformed(InputAction.CallbackContext ctx)
    {
        OnSprint?.Invoke(true);
    }
    private void SprintCanceled(InputAction.CallbackContext ctx)
    {
        OnSprint?.Invoke(false);
    }

    private void JumpPerformed(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke(true);
    }
    private void JumpCanceled(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke(false);
    }
}