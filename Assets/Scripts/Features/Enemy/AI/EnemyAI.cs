using UnityEngine;
using UnityEngine.AI;

public class EnemyAI
{
    private NavMeshAgent _agent;
    private Transform _player;
    private Transform _transform;
    private LayerMask _whatIsGround;
    private LayerMask _whatIsPlayer;

    // Patroling
    private Vector3 _walkPoint;
    private bool _walkPointSet;
    private float _walkPointRange = 5f;

    // Attacking
    private float _timeBetweenAttacks = 0.5f;
    private bool _alreadyAttacked;

    // States
    private float _sightRange = 20f, _attackRange = 12f;
    private bool _playerInSightRange, _playerInAttackRange;

    public EnemyAI(NavMeshAgent agent, Transform player, Transform transform, LayerMask whatIsGround, LayerMask whatIsPlayer)
    {
        _agent = agent;
        _player = player;
        _transform = transform;
        _whatIsGround = whatIsGround;
        _whatIsPlayer = whatIsPlayer;
    }

    public float GetSightRange()
    {
        return _sightRange;
    }

    public float GetAttackRange()
    {
        return _attackRange;
    }
    
    public LayerMask GetWhatIsPlayer()
    {
        return _whatIsPlayer;
    }

    public bool GetPlayerInSightRange()
    {
        return _playerInSightRange;
    }

    public bool GetPlayerInAttackRange()
    {
        return _playerInAttackRange;
    }

    public void SetPlayerInSightRange(bool value)
    {
        _playerInSightRange = value;
    }

    public void SetPlayerInAttackRange(bool value)
    {
        _playerInAttackRange = value;
    }

    public void Patroling()
    {
        if (!_walkPointSet) SearchWalkPoint();

        if (_walkPointSet)
        {
            _agent.SetDestination(_walkPoint);
        }

        Vector3 distanceToWalkPoint = _transform.position - _walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            _walkPointSet = false;
        }
    }

    public void Chasing()
    {
        _agent.SetDestination(_player.position);
    }

    public void Attacking()
    {
        // set enemy to stop walking
        _agent.SetDestination(_transform.position);
        _transform.LookAt(_player);

        if (!_alreadyAttacked)
        {
            // Attack code here
            Debug.Log("Enemy attacking!");

            _alreadyAttacked = true;
            // Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
        float randmoX = Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(_transform.position.x + randmoX, _transform.position.y, _transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -_transform.up, 2f, _whatIsGround))
        {
            _walkPointSet = true;
        }
    }
}
