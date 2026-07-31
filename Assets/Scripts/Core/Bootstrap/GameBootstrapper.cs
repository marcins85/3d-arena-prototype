using UnityEngine;
using UnityEngine.InputSystem;

public class GameBootstrappper : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private EnemyController _enemy;
    [SerializeField] private Hitbox _enemyRightHandHitbox;
    [SerializeField] private Hitbox _playerSwordHitbox;

    public void Awake()
    {
        var playerConfig = _player.GetPlayerConfigSO();
        var cc = _player.GetCharacterController();
        var asset = _player.GetInputActionAsset();
        var camRoot = _player.GetCamRoot();
        var camPitch = _player.GetCamPitch();
        var animator = _player.GetAnimator();
        var mask = _player.GetLayerMask();

        var enemyConfig = _enemy.GetEnemyConfigSO();
        var enemyAgent = _enemy.GetNavMeshAgent();
        var enemyPlayerTransform = _enemy.GetPlayerTransform();
        var enemyLayerMaskGround = _enemy.GetLayerMaskGround();
        var enemyLayerMaskPlayer = _enemy.GetLayerMaskPlayer();
        var enemyAnimator = _enemy.GetAnimator();

        IPlayerInput input = new PlayerInput(asset);
        IInputBuffer inputBuffer = new InputBufferSystem();

        IMovement playerMovement = new PlayerMovement(cc, _player.transform, camRoot, mask, playerConfig);
        IRotation playerRotation = new PlayerRotation(camRoot, camPitch, _player.transform, playerConfig);
        IJump playerJump = new PlayerJump(playerConfig);
        IAttack playerAttack = new PlayerAttack(_playerSwordHitbox);
        IHealth playerHealth = new Health(playerConfig.Health);

        IMovement enemyMovement = new EnemyMovement(enemyAgent, enemyConfig);
        IRotation enemyRotation = new NoOpRotation();
        IJump enemyJump = null;
        IAttack enemyAttack = new EnemyAttack(_enemyRightHandHitbox);
        IHealth enemyHealth = new Health(enemyConfig.Health);

        IAnimationSystem playerAnimation = new AnimationSystem(playerConfig, playerMovement, animator, playerJump, playerRotation, playerAttack);
        IAnimationSystem enemyAnimation = new AnimationSystem(enemyConfig, enemyMovement, enemyAnimator, enemyJump, enemyRotation, enemyAttack);
        ITurnHandler turnHandler = playerRotation as ITurnHandler;

        EnemyAI enemyAI = new EnemyAI(enemyAgent, enemyPlayerTransform, _enemy.transform, enemyLayerMaskGround, enemyLayerMaskPlayer);

        _player.Inject(playerMovement, playerRotation, playerJump, turnHandler, input, inputBuffer, playerAnimation, playerHealth);
        _enemy.Inject(enemyAI, enemyAnimation, enemyHealth);
    }
}
