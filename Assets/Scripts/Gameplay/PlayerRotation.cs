using UnityEngine;

public class PlayerRotation : IRotation
{
    private PlayerConfigSO _config;
    private Transform _transform;
    private Transform _camTarget;
    private float _verticalRotation;
    private Vector2 _lookInput;

    public PlayerRotation(Transform camTarget, Transform transform, PlayerConfigSO config)
    {
        _camTarget = camTarget;
        _transform = transform;
        _config = config;
    }

    public void SetLookInput(Vector2 input)
    {
        _lookInput = input;
    }

    private void HorizontalRotation(float amount)
    {
        if (_transform != null)
        {
            _transform.Rotate(0f, amount, 0f);
        }
    }

    private void VerticalRotation(float amount)
    {
        _verticalRotation = Mathf.Clamp(_verticalRotation - amount, -_config.upDownLimit, _config.upDownLimit);
        if (_camTarget != null)
            _camTarget.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    public void HandleRotation()
    {
        float l_mouseYRotation = _lookInput.y * _config.mouseSensitivity;
        float l_mouseXRotation = _lookInput.x * _config.mouseSensitivity;

        HorizontalRotation(l_mouseXRotation);
        VerticalRotation(l_mouseYRotation);
    }
}
