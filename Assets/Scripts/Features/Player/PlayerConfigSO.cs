using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfigSO", menuName = "Scriptable Objects/PlayerConfigSO")]
public class PlayerConfigSO : ScriptableObject
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintMultiplier = 3.5f;

    [Header("Jump")]
    public float jumpForce = 5f;
    public float gravityMultiplier = 1f;

    [Header("Rotation")]
    public float mouseSensitivity = 0.1f;
    public float upDownLimit = 80f;
    public float moveTurnTreshold = 45f;
}
