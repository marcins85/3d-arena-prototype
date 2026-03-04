using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : IPlayerInput
{
    private readonly string m_mapName = "Main";
    private readonly string m_moveName = "Move";
    private readonly string m_lookName = "Look";
    private readonly string m_sprintName = "Sprint";
    private readonly string m_jumpName = "Jump";

    private readonly InputAction m_moveAction;
    private readonly InputAction m_lookAction;
    private readonly InputAction m_sprintAction;
    private readonly InputAction m_jumpAction;

    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action<bool> OnSprint;
    public event Action<bool> OnJump;

    public PlayerInput(InputActionAsset p_asset)
    {
        var l_map = p_asset.FindActionMap(m_mapName);
        m_moveAction = l_map.FindAction(m_moveName);
        m_lookAction = l_map.FindAction(m_lookName);
        m_sprintAction = l_map.FindAction(m_sprintName);
        m_jumpAction = l_map.FindAction(m_jumpName);

        m_moveAction.performed += MovePerformed;
        m_moveAction.canceled += MoveCanceled;

        m_lookAction.performed += LookPerformed;
        m_lookAction.canceled += LookCanceled;

        m_sprintAction.performed += SprintPerformed;
        m_sprintAction.canceled += SprintCanceled;

        m_jumpAction.performed += JumpPerformed;
        m_jumpAction.canceled += JumpCanceled;
    }

    public void Enable()
    {
        m_moveAction.Enable();
        m_lookAction.Enable();
        m_sprintAction.Enable();
        m_jumpAction.Enable();
    }
    public void Disable()
    {
        m_moveAction.Disable();
        m_lookAction.Disable();
        m_sprintAction.Disable();
        m_jumpAction.Disable();
    }

    private void MovePerformed(InputAction.CallbackContext p_ctx)
    {
        OnMove?.Invoke(p_ctx.ReadValue<Vector2>());
    }
    private void MoveCanceled(InputAction.CallbackContext p_ctx)
    {
        OnMove?.Invoke(Vector2.zero);
    }

    private void LookPerformed(InputAction.CallbackContext p_ctx)
    {
        OnLook?.Invoke(p_ctx.ReadValue<Vector2>());
    }
    private void LookCanceled(InputAction.CallbackContext p_ctx)
    {
        OnLook?.Invoke(Vector2.zero);
    }

    private void SprintPerformed(InputAction.CallbackContext p_ctx)
    {
        OnSprint?.Invoke(true);
    }
    private void SprintCanceled(InputAction.CallbackContext p_ctx)
    {
        OnSprint?.Invoke(false);
    }

    private void JumpPerformed(InputAction.CallbackContext p_ctx)
    {
        OnJump?.Invoke(true);
    }
    private void JumpCanceled(InputAction.CallbackContext p_ctx)
    {
        OnJump?.Invoke(false);
    }
}