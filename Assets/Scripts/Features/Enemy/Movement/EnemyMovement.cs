using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : IMovement
{
    private NavMeshAgent _agent;
    private IEntityConfig _config;

    public float CurrentVerticalVelocity => 0f; // NavMesh nie potrzebuje
    public MovementState State { get; set; } = MovementState.Normal;
    public bool CanMove => State == MovementState.Normal;

    public EnemyMovement(NavMeshAgent agent, IEntityConfig config)
    {
        _agent = agent;
        _config = config;
    }

    public void SetMoveInput(Vector2 input) { } // Enemy nie używa input
    public void SetSprintTrigger(bool trigger) { }

    public void HandleMovement(float verticalVelocity)
    {
        // NavMeshAgent sam się porusza
    }

    public bool IsGroundedRaycast() => true; // NavMesh zawsze na ziemi
}