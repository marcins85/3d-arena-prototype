using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfigSO", menuName = "Scriptable Objects/PlayerConfigSO")]
public class PlayerConfigSO : ScriptableObject, IEntityConfig
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 3.5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravityMultiplier = 1f;

    [Header("Rotation")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float upDownLimit = 80f;
    [SerializeField] private float moveTurnTreshold = 45f;

    [Header("Health")]
    [SerializeField] private int health = 100;

    public float WalkSpeed => walkSpeed;
    public float SprintMultiplier => sprintMultiplier;
    public float JumpForce => jumpForce;
    public float GravityMultiplier => gravityMultiplier;
    public float MouseSensitivity => mouseSensitivity;
    public float UpDownLimit => upDownLimit;
    public float MoveTurnTreshold => moveTurnTreshold;
    public int Health => health;
}
