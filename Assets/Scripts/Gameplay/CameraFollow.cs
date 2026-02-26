using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _camTarget;
    [SerializeField] private float _pLerp = .02f;
    [SerializeField] private float _rLerp = .01f;

    private void LateUpdate()
    {
        if (_camTarget == null) return;

        transform.position = Vector3.Lerp(transform.position, _camTarget.position, _pLerp);
        transform.rotation = Quaternion.Slerp(transform.rotation, _camTarget.rotation, _rLerp);
    }
}