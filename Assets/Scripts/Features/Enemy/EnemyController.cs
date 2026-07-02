using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO _config;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsPlayer;

    private IAnimationSystem _animation;
    private EnemyAI _ai;

    public void Inject(EnemyAI ai, IAnimationSystem animation)
    {
        _ai = ai;
        _animation = animation;
    }

    private void Update()
    {
        _ai.SetPlayerInSightRange(Physics.CheckSphere(transform.position, _ai.GetSightRange(), _ai.GetWhatIsPlayer()));
        _ai.SetPlayerInAttackRange(Physics.CheckSphere(transform.position, _ai.GetAttackRange(), _ai.GetWhatIsPlayer()));

        if (!_ai.GetPlayerInSightRange() && !_ai.GetPlayerInAttackRange()) _ai.Patroling();
        if (_ai.GetPlayerInSightRange() && !_ai.GetPlayerInAttackRange()) _ai.Chasing();
        if (_ai.GetPlayerInSightRange() && _ai.GetPlayerInAttackRange())
        {
            _ai.Attacking();

            if (_ai.TryAttack())
            {
                _animation.RequestAttack2();

                StartCoroutine(ResetAttackCoroutine());
            }
        }

        _animation.Update(new Vector2(0, 0), true, 0f, false);
    }

    private IEnumerator ResetAttackCoroutine()
    {
        yield return new WaitForSeconds(_ai.GetTimeBetweenAttacks());
        _ai.ResetAttack();
    }

    public void OnAttackHitted()
    {
        _animation.OnAttackHitted();
    }

    public void OnAttackFinished()
    {
        _animation.OnAttackFinished();
    }

    public void OnAttackComboTransition()
    {
        _animation.ComboTransition();
    }

    public void OnAttackComboWindowOpen()
    {
        _animation.ComboWindowOpen();
    }

    public void OnBlockWindowClosed()
    {
        _animation.BlockWindowClosed();
    }

    public EnemyConfigSO GetEnemyConfigSO() => _config;
    public NavMeshAgent GetNavMeshAgent() => _agent;
    public Transform GetPlayerTransform() => _player;
    public LayerMask GetLayerMaskGround() => _whatIsGround;
    public LayerMask GetLayerMaskPlayer() => _whatIsPlayer;
    public Animator GetAnimator() => _animator;
}
