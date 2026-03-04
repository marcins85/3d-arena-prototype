using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : IMovement
{
    private PlayerConfigSO m_config;
    private CharacterController m_characterController;
    private Vector3 m_worldDirection;
    private Vector3 m_currentMovement;
    private Vector3 m_airMovement;
    private Transform m_transform;
    private Transform m_camRoot;
    private Vector2 m_moveInput;
    private LayerMask m_groundMask;
    private bool m_sprintTrigger;

    public float CurrentVerticalVelocity => m_currentMovement.y;
    public bool CanMove { get; set; } = true;

    public PlayerMovement(CharacterController p_characterController, Transform p_transform, Transform p_camRoot, LayerMask p_groundMask, PlayerConfigSO p_config)
    {
        m_characterController = p_characterController;
        m_transform = p_transform;
        m_camRoot = p_camRoot;
        m_groundMask = p_groundMask;
        m_config = p_config;
    }

    // WSAD podąża za playerem
    //private Vector3 CalculateWorldDirection()
    //{
    //    Vector3 l_inputDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
    //    Vector3 l_worldDirection = _transform.TransformDirection(l_inputDirection);
    //    return l_worldDirection.normalized;
    //}

    // WSAD podąża za kamerą
    private Vector3 CalculateWorldDirection()
    {
        Vector3 l_input = new Vector3(m_moveInput.x, 0f, m_moveInput.y);

        Vector3 l_camForward = m_camRoot.forward;
        l_camForward.y = 0;
        l_camForward.Normalize();

        Vector3 l_camRight = m_camRoot.right;
        l_camRight.y = 0;
        l_camRight.Normalize();

        Vector3 l_worldDir = l_camForward * l_input.z + l_camRight * l_input.x;
        return l_worldDir.normalized;
    }

    private bool IsGroundedRaycast()
    {
        Vector3 l_origin = m_characterController.bounds.center;
        l_origin.y = m_characterController.bounds.min.y + 0.05f;

        return Physics.Raycast(
            l_origin,
            Vector3.down,
            0.3f,
            m_groundMask
        );
    }

    public void SetMoveInput(Vector2 p_input)
    {
        m_moveInput = p_input;
    }

    public void SetSprintTrigger(bool p_trigger)
    {
        m_sprintTrigger = p_trigger;
    }

    public void HandleMovement(float p_verticalVelocity)
    {
        if (!m_characterController) return;

        if (!CanMove)
        {
            m_currentMovement.x = 0;
            m_currentMovement.z = 0;

            p_verticalVelocity += Physics.gravity.y * m_config.gravityMultiplier * Time.deltaTime;
            if (p_verticalVelocity < 0f)
                p_verticalVelocity = -2f;

            m_currentMovement.y = p_verticalVelocity;
            m_characterController.Move(m_currentMovement * Time.deltaTime);
            return;
        }


        if (IsGroundedRaycast())
        {
            float l_moveSpeed = m_config.walkSpeed * (m_sprintTrigger ? m_config.sprintMultiplier : 1);

            Vector3 l_worldDirection = CalculateWorldDirection();
            m_currentMovement.x = l_worldDirection.x * l_moveSpeed;
            m_currentMovement.z = l_worldDirection.z * l_moveSpeed;
            m_airMovement = new Vector3(m_currentMovement.x, 0f, m_currentMovement.z);
        }
        else
        {
            m_currentMovement.x = m_airMovement.x;
            m_currentMovement.z = m_airMovement.z;
        }

        if (IsGroundedRaycast())
        {
            if (p_verticalVelocity < 0f)
            {
                p_verticalVelocity = -2f;
            }
        }
        else
        {
            p_verticalVelocity += Physics.gravity.y * m_config.gravityMultiplier * Time.deltaTime;
        }

        m_currentMovement.y = p_verticalVelocity;
        m_characterController.Move(m_currentMovement * Time.deltaTime);
    }
}
