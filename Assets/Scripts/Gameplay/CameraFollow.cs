using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform camTarget;
    [SerializeField] private float pLerp = .02f;
    [SerializeField] private float rLerp = .01f;

    private void LateUpdate()
    {
        if (camTarget == null) return;

        transform.position = Vector3.Lerp(transform.position, camTarget.position, pLerp);
        transform.rotation = Quaternion.Slerp(transform.rotation, camTarget.rotation, rLerp);
    }
}
