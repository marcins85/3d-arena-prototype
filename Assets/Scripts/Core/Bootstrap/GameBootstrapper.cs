using UnityEngine;
using UnityEngine.InputSystem;

public class GameBootstrappper : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    public void Awake()
    {
        var config = _player.GetPlayerConfigSO();
        var cc = _player.GetCharacterController();
        var asset = _player.GetInputActionAsset();
        var camRoot = _player.GetCamRoot();
        var camPitch = _player.GetCamPitch();
        var animator = _player.GetAnimator();
        var mask = _player.GetLayerMask();

        IAnimationSystem animation = new AnimationSystem(animator);
        IPlayerInput input = new PlayerInput(asset);
        IMovement movement = new PlayerMovement(cc, _player.transform, camRoot, mask, config);
        IRotation rotation = new PlayerRotation(camRoot, camPitch, _player.transform, config);
        IJump jump = new PlayerJump(config);
        ITurnHandler turnHandler = rotation as ITurnHandler;

        rotation.OnTurnStartedEvent += (right) =>
        {
            movement.CanMove = false;
            animation.SetTurn(right);
        };

        rotation.OnTurnFinishedEvent += () => movement.CanMove = true;

        _player.Inject(movement, rotation, jump, turnHandler, input, animation);
    }
}
