using UnityEngine;
using UnityEngine.InputSystem;

public class EnemeyController : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO _config;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundMask;

    private IEnemyInput _input;
    private IInputBuffer _inputBuffer;
    private IMovement _movement;
    private IRotation _rotation;
    private IJump _jump;
    private IAnimationSystem _animation;
    private IDamage _damage;

    private bool _jumpRequest = false;
    private Vector2 _moveInput;

    public void Inject(IMovement movement, IRotation rotation, IJump jump, ITurnHandler turnHandler, IEnemyInput input, IInputBuffer inputBuffer, IAnimationSystem animation)
    {
        _movement = movement;
        _rotation = rotation;
        _jump = jump;
        _input = input;
        _inputBuffer = inputBuffer;
        _animation = animation;
    }
}
