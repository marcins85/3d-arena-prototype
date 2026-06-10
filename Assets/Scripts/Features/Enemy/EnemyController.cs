using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO _config;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _target;

    private IMovement _movement;
    private IRotation _rotation;
    private IEnemyAI _enemyAI;
    private IAnimationSystem _animation;
    private IDamage _damage;

    private float _verticalVelocity;
    private bool _isGrounded;

    private void Awake()
    {
        _characterController ??= GetComponent<CharacterController>();
        _animator ??= GetComponentInChildren<Animator>();

        _movement = new EnemyMovement(_characterController, _config, _groundMask);
        _rotation = new EnemyRotation(transform, _config);
        _enemyAI = new EnemyAI(_target, _config);
        _animation ??= new AnimationSystem(_animator);

        // Jeśli masz już system obrażeń, wstrzyknij go tutaj
        _damage ??= new DamageSystem();
    }

    private void Update()
    {
        _isGrounded = _movement.CheckGrounded();
        _verticalVelocity = _movement.ApplyGravity(_verticalVelocity, _isGrounded);

        Vector2 moveInput = _enemyAI.GetMoveInput();
        bool jumpRequest = _enemyAI.ShouldJump(_isGrounded);
        bool attackRequest = _enemyAI.ShouldAttack();
        bool blockRequest = _enemyAI.ShouldBlock();

        if (jumpRequest)
            _verticalVelocity = _movement.Jump();

        Vector3 moveDirection = _rotation.GetMoveDirection(moveInput);
        _movement.HandleMovement(moveDirection, _verticalVelocity);
        _rotation.HandleRotation(moveDirection, _target.position);

        _animation.SetSpeed(moveInput.magnitude);
        _animation.SetGrounded(_isGrounded);

        if (attackRequest) _animation.PlayAttack();
        if (blockRequest) _animation.PlayBlock();
    }

    public void Inject(IAnimationSystem animation, IDamage damage)
    {
        _animation = animation;
        _damage = damage;
    }
}
