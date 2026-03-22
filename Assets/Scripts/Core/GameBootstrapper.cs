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
        IRotation rotation = new PlayerRotation(camRoot, camPitch, _player.transform, animation, movement, config);
        IJump jump = new PlayerJump(config);
        ITurnHandler turnHandler = rotation as ITurnHandler;

        _player.Inject(movement, rotation, jump, turnHandler, input, animation);
    }
}
