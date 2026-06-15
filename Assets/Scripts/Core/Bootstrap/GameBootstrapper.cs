using UnityEngine;
using UnityEngine.InputSystem;

public class GameBootstrappper : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private EnemyController _enemy;

    public void Awake()
    {
        var config = _player.GetPlayerConfigSO();
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

        IPlayerInput input = new PlayerInput(asset);
        IInputBuffer inputBuffer = new InputBufferSystem();
        IMovement movement = new PlayerMovement(cc, _player.transform, camRoot, mask, config);
        IRotation rotation = new PlayerRotation(camRoot, camPitch, _player.transform, config);
        IJump jump = new PlayerJump(config);
        IAnimationSystem animation = new AnimationSystem(config, movement, animator, jump, rotation);
        ITurnHandler turnHandler = rotation as ITurnHandler;

        EnemyAI enemyAI = new EnemyAI(enemyAgent, enemyPlayerTransform, _enemy.transform, enemyLayerMaskGround, enemyLayerMaskPlayer);

        _player.Inject(movement, rotation, jump, turnHandler, input, inputBuffer, animation);
        _enemy.Inject(enemyAI, animation);
    }
}
