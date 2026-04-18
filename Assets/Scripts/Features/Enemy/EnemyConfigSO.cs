using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfigSO", menuName = "Scriptable Objects/EnemyConfigSO")]
public class EnemyConfigSO : ScriptableObject
{
    [Header("Movement")]
    public float walkSpeed = 1.5f;
    public float sprintMultiplier = 2.0f;

    [Header("Jump")]
    public float jumpForce = 3f;
    public float gravityMultiplier = 0.4f;

    [Header("Rotation")]
    public float mouseSensitivity = 0.1f;
    public float upDownLimit = 20f;
    public float moveTurnTreshold = 45f;
}
