using UnityEngine;

public class PlayerRotation
{
    private PlayerInput _input;
    private Transform _transform;
    private Transform _camTarget;
    private float _verticalRotation;
    private float _upDownLimit = 80f;
    private float _mouseSensitivity = 0.1f;

    public PlayerRotation(PlayerInput input, Transform camTarget, Transform transform)
    {
        _input = input;
        _camTarget = camTarget;
        _transform = transform;
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
        _verticalRotation = Mathf.Clamp(_verticalRotation - amount, -_upDownLimit, _upDownLimit);
        if (_camTarget != null)
            _camTarget.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    public void HandleRotation()
    {
        float l_mouseXRotation = _input.LookInput.x * _mouseSensitivity;
        float l_mouseYRotation = _input.LookInput.y * _mouseSensitivity;

        HorizontalRotation(l_mouseXRotation);
        VerticalRotation(l_mouseYRotation);
    }
}
