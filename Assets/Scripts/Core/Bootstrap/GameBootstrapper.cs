using UnityEngine;
using UnityEngine.InputSystem;

public class GameBootstrappper : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private EnemyController _enemy;

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

        IMovement enemyMovement = new EnemyMovement(enemyAgent, enemyConfig);
        IRotation enemyRotation = new NoOpRotation();
        IJump enemyJump = null;

        IAnimationSystem playerAnimation = new AnimationSystem(playerConfig, playerMovement, animator, playerJump, playerRotation);
        IAnimationSystem enemyAnimation = new AnimationSystem(enemyConfig, enemyMovement, enemyAnimator, enemyJump, enemyRotation);
        ITurnHandler turnHandler = playerRotation as ITurnHandler;

        EnemyAI enemyAI = new EnemyAI(enemyAgent, enemyPlayerTransform, _enemy.transform, enemyLayerMaskGround, enemyLayerMaskPlayer);

        _player.Inject(playerMovement, playerRotation, playerJump, turnHandler, input, inputBuffer, playerAnimation);
        _enemy.Inject(enemyAI, enemyAnimation);
    }
}
