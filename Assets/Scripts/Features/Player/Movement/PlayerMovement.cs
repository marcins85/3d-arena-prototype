using UnityEngine;

public class PlayerMovement : IMovement
{
    private PlayerConfigSO _config;
    private CharacterController _characterController;
    private Vector3 _worldDirection;
    private Vector3 _currentMovement;
    private Vector3 _airMovement;
    private Transform _transform;
    private Transform _camRoot;
    private Vector2 _moveInput;
    private LayerMask _groundMask;
    private bool _sprintTrigger;

    public float CurrentVerticalVelocity => _currentMovement.y;
    public bool CanMove { get; set; } = true;

    public PlayerMovement(CharacterController characterController, Transform transform, Transform camRoot, LayerMask groundMask, PlayerConfigSO config)
    {
        _characterController = characterController;
        _transform = transform;
        _camRoot = camRoot;
        _groundMask = groundMask;
        _config = config;
    }

    // WSAD podąża za playerem
    //private Vector3 CalculateWorldDirection()
    //{
    //    Vector3 inputDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
    //    Vector3 worldDirection = _transform.TransformDirection(inputDirection);
    //    return worldDirection.normalized;
    //}

    // WSAD podąża za kamerą
    private Vector3 CalculateWorldDirection()
    {
        Vector3 input = new Vector3(_moveInput.x, 0f, _moveInput.y);

        Vector3 camForward = _camRoot.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = _camRoot.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 worldDir = camForward * input.z + camRight * input.x;
        return worldDir.normalized;
    }

    public bool IsGroundedRaycast()
    {
        Vector3 origin = _characterController.bounds.center;
        origin.y = _characterController.bounds.min.y + 0.05f;

        return Physics.Raycast(
            origin,
            Vector3.down,
            0.3f,
            _groundMask
        );

        //return Physics.SphereCast(
        //    _characterController.bounds.center,
        //    _characterController.radius * 0.9f,
        //    Vector3.down,
        //    out _,
        //    0.2f,
        //    _groundMask
        //);

    }

    public void SetMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    public void SetSprintTrigger(bool trigger)
    {
        _sprintTrigger = trigger;
    }

    public void HandleMovement(float verticalVelocity)
    {
        if (!_characterController) return;

        if (!CanMove)
        {
            _currentMovement.x = 0;
            _currentMovement.z = 0;
            _currentMovement.y = verticalVelocity;

            _characterController.Move(_currentMovement * Time.deltaTime);
            return;
        }


        if (IsGroundedRaycast())
        {

            float moveSpeed = _config.walkSpeed * (_sprintTrigger ? _config.sprintMultiplier : 1);

            Vector3 worldDirection = CalculateWorldDirection();
            _currentMovement.x = worldDirection.x * moveSpeed;
            _currentMovement.z = worldDirection.z * moveSpeed;
            _airMovement = new Vector3(_currentMovement.x, 0f, _currentMovement.z);
        }
        else
        {
            _currentMovement.x = _airMovement.x;
            _currentMovement.z = _airMovement.z;
        }

        _currentMovement.y = verticalVelocity;

        //_currentMovement = Vector3.Lerp(
        //    _currentMovement,
        //    targetMovement,
        //    _config.acceleration * Time.deltaTime
        //);

        _characterController.Move(_currentMovement * Time.deltaTime);
    }
}
