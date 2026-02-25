using UnityEngine;

public class PlayerRotation
{
    private Transform _transform;
    private Transform _camTarget;
    private float _verticalRotation;
    private float _upDownLimit = 80f;
    private float _mouseSensitivity = 0.1f;
    private Vector2 _lookInput;

    public PlayerRotation(Transform camTarget, Transform transform)
    {
        _camTarget = camTarget;
        _transform = transform;
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
        _verticalRotation = Mathf.Clamp(_verticalRotation - amount, -_upDownLimit, _upDownLimit);
        if (_camTarget != null)
            _camTarget.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    public void HandleRotation()
    {
        float l_mouseXRotation = _lookInput.x * _mouseSensitivity;
        float l_mouseYRotation = _lookInput.y * _mouseSensitivity;

        HorizontalRotation(l_mouseXRotation);
        VerticalRotation(l_mouseYRotation);
    }
}
