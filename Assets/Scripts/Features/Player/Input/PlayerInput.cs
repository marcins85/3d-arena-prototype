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
    private readonly string _attack1Name = "Attack1";
    private readonly string _attack2Name = "Attack2";

    private readonly InputAction _moveAction;
    private readonly InputAction _lookAction;
    private readonly InputAction _sprintAction;
    private readonly InputAction _jumpAction;
    private readonly InputAction _attack1Action;
    private readonly InputAction _attack2Action;

    public bool BlockMovementInput { get; set; }

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<bool> OnSprint;
    public event Action<bool> OnJump;
    public event Action<bool> OnAttack1;
    public event Action<bool> OnAttack2;

    public PlayerInput(InputActionAsset asset)
    {
        var map = asset.FindActionMap(_mapName);
        _moveAction = map.FindAction(_moveName);
        _lookAction = map.FindAction(_lookName);
        _sprintAction = map.FindAction(_sprintName);
        _jumpAction = map.FindAction(_jumpName);
        _attack1Action = map.FindAction(_attack1Name);
        _attack2Action = map.FindAction(_attack2Name);

        _moveAction.performed += MovePerformed;
        _moveAction.canceled += MoveCanceled;

        _lookAction.performed += LookPerformed;
        _lookAction.canceled += LookCanceled;

        _sprintAction.performed += SprintPerformed;
        _sprintAction.canceled += SprintCanceled;

        _jumpAction.performed += JumpPerformed;
        _jumpAction.canceled += JumpCanceled;

        _attack1Action.performed += Attack1Performed;
        _attack1Action.canceled += Attack1Canceled;

        _attack2Action.performed += Attack2Performed;
        _attack2Action.canceled += Attack2Canceled;
    }

    public void Enable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
        _sprintAction.Enable();
        _jumpAction.Enable();
        _attack1Action.Enable();
        _attack2Action.Enable();
    }
    public void Disable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
        _sprintAction.Disable();
        _jumpAction.Disable();
        _attack1Action.Disable();
        _attack2Action.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        if (BlockMovementInput)
        {
            OnMove?.Invoke(Vector2.zero);
            return;
        }
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
        OnSprint?.Invoke(ctx.ReadValueAsButton());
    }
    private void SprintCanceled(InputAction.CallbackContext ctx)
    {
        OnSprint?.Invoke(false);
    }

    private void JumpPerformed(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke(ctx.ReadValueAsButton());
    }
    private void JumpCanceled(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke(false);
    }

    private void Attack1Performed(InputAction.CallbackContext ctx)
    {
        OnAttack1?.Invoke(ctx.ReadValueAsButton());
    }
    private void Attack1Canceled(InputAction.CallbackContext ctx)
    {
        OnAttack1?.Invoke(false);
    }

    private void Attack2Performed(InputAction.CallbackContext ctx)
    {
        OnAttack2?.Invoke(ctx.ReadValueAsButton());
    }
    private void Attack2Canceled(InputAction.CallbackContext ctx)
    {
        OnAttack2?.Invoke(false);
    }
}