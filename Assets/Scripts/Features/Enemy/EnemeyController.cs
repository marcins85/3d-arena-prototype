using UnityEngine;
using UnityEngine.InputSystem;

public class EnemeyController : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO _config;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundMask;

    private IAnimationSystem _animation;
    private IDamage _damage;

    private bool _jumpRequest = false;
    private Vector2 _moveInput;

    public void Inject(IAnimationSystem animation)
    {
        _animation = animation;
    }
}
