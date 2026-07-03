using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfigSO", menuName = "Scriptable Objects/EnemyConfigSO")]
public class EnemyConfigSO : ScriptableObject, IEntityConfig
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float sprintMultiplier = 2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float gravityMultiplier = 0.4f;

    [Header("Rotation")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float upDownLimit = 20f;
    [SerializeField] private float moveTurnTreshold = 45f;

    public float WalkSpeed => walkSpeed;
    public float SprintMultiplier => sprintMultiplier;
    public float JumpForce => jumpForce;
    public float GravityMultiplier => gravityMultiplier;
    public float MouseSensitivity => mouseSensitivity;
    public float UpDownLimit => upDownLimit;
    public float MoveTurnTreshold => moveTurnTreshold;
}
