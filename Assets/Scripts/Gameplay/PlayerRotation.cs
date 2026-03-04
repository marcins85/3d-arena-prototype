using UnityEngine;

public interface ITurnHandler
{
    void OnTurnFinished(bool p_right);
}

public class PlayerRotation : IRotation, ITurnHandler
{
    private PlayerConfigSO m_config;

    private Transform m_player;
    private Transform m_camRoot;
    private Transform m_camPitch;

    private Animator m_animator;
    private IMovement m_movement;

    private float m_verticalRotation;
    private Vector2 m_lookInput;

    private const float ROTATE_SPEED = 120f;
    private const float MOVE_TURN_THRESHOLD = 45f;
    private bool m_wantsToMoveForward;
    private bool m_isMoving;

    public bool IsTurning { get; private set; }

    public PlayerRotation(Transform p_camRoot, Transform p_camPitch, Transform p_player, Animator p_animator, IMovement p_movement, PlayerConfigSO p_config)
    {
        m_camRoot = p_camRoot;
        m_camPitch = p_camPitch;
        m_player = p_player;
        m_animator = p_animator;
        m_movement = p_movement;
        m_config = p_config;
    }

    public void SetLookInput(Vector2 p_input)
    {
        m_lookInput = p_input;
    }

    public void SetMoveInput(Vector2 p_moveInput)
    {
        m_isMoving = p_moveInput != Vector2.zero;
        m_wantsToMoveForward = p_moveInput.y > 0.1f;
    }

    public void HandleRotation()
    {
        float l_mouseX = m_lookInput.x * m_config.mouseSensitivity;
        float l_mouseY = m_lookInput.y * m_config.mouseSensitivity;

        RotateCameraYaw(l_mouseX);
        RotateCameraPitch(l_mouseY);

        HandlePlayerRotation();
    }

    private void RotateCameraYaw(float p_amount)
    {
        m_camRoot.Rotate(0f, p_amount, 0f, Space.World);
    }

    private void RotateCameraPitch(float p_amount)
    {
        m_verticalRotation = Mathf.Clamp(m_verticalRotation - p_amount, -m_config.upDownLimit, m_config.upDownLimit);
        m_camPitch.localRotation = Quaternion.Euler(m_verticalRotation, 0f, 0f);
    }

    private void HandlePlayerRotation()
    {
        float l_targetYaw = m_camRoot.eulerAngles.y;
        float l_currentYaw = m_player.eulerAngles.y;
        float l_delta = Mathf.DeltaAngle(l_currentYaw, l_targetYaw);

        // TURN-IN-PLACE gdy stoi
        if (!IsTurning && m_wantsToMoveForward)
        {
            if (l_delta > MOVE_TURN_THRESHOLD)
            {
                StartTurn(true);
                return;
            }
            else if (l_delta < -MOVE_TURN_THRESHOLD)
            {
                StartTurn(false);
                return;
            }
        }

        // AUTO-ROTATE — gdy idzie
        if (m_isMoving && !IsTurning)
        {
            float l_newYaw = Mathf.MoveTowardsAngle(l_currentYaw, l_targetYaw, ROTATE_SPEED * Time.deltaTime);
            m_player.rotation = Quaternion.Euler(0, l_newYaw, 0);
            return;
        }
    }

    public void StartTurn(bool p_right)
    {
        IsTurning = true;
        m_movement.CanMove = false;
        m_animator.SetTrigger(p_right ? "TurnRight" : "TurnLeft");
    }

    public void OnTurnFinished(bool p_right)
    {
        //float targetYaw = _camRoot.eulerAngles.y;
        //_player.rotation = Quaternion.Euler(0, targetYaw, 0);

        IsTurning = false;
        m_movement.CanMove = true;
    }
}