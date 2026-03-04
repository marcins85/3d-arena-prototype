using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO m_config;
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private InputActionAsset m_asset;
    [SerializeField] private Transform m_camRoot;
    [SerializeField] private Transform m_camPitch;
    [SerializeField] private Animator m_animator;
    [SerializeField] private LayerMask m_groundMask;
    private IPlayerInput m_input;
    private IMovement m_movement;
    private IRotation m_rotation;
    private IJump m_jump;

    public ITurnHandler turnHandler;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        m_input = new PlayerInput(m_asset);
        m_movement = new PlayerMovement(m_characterController, transform, m_camRoot, m_groundMask, m_config);

        var l_rotation = new PlayerRotation(m_camRoot, m_camPitch, transform, m_animator, m_movement, m_config);
        m_rotation = l_rotation;
        turnHandler = l_rotation;

        m_jump = new PlayerJump(m_config);
    }

    private void Update()
    {
        bool l_grounded = m_characterController.isGrounded;
        m_jump.HandleJump(l_grounded);
        float l_y = m_jump.GetVerticalVelocity();
        m_movement.HandleMovement(l_y);
        m_jump.SetVerticalVelocity(m_movement.CurrentVerticalVelocity);
        m_rotation.HandleRotation();
    }

    private void OnEnable()
    {
        m_input.Enable();

        m_input.OnMove += OnMove;
        m_input.OnLook += m_rotation.SetLookInput;
        m_input.OnSprint += m_movement.SetSprintTrigger;
        m_input.OnJump += m_jump.SetJumpTrigger;
    }
    private void OnDisable()
    {
        m_input.OnMove -= OnMove;
        m_input.OnLook -= m_rotation.SetLookInput;
        m_input.OnSprint -= m_movement.SetSprintTrigger;
        m_input.OnJump -= m_jump.SetJumpTrigger;

        m_input.Disable();
    }

    private void OnMove(Vector2 p_velocity)
    {
        m_movement.SetMoveInput(p_velocity);
        ((PlayerRotation)m_rotation).SetMoveInput(p_velocity);
    }

    public void OnTurnLeftFinished()
    {
        turnHandler?.OnTurnFinished(false);
    }

    public void OnTurnRightFinished()
    {
        turnHandler?.OnTurnFinished(true);
    }
}