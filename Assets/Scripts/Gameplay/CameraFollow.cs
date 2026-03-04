using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_camTarget;
    [SerializeField] private float m_pLerp = .02f;
    [SerializeField] private float m_rLerp = .01f;

    private void LateUpdate()
    {
        if (m_camTarget == null) return;

        transform.position = Vector3.Lerp(transform.position, m_camTarget.position, m_pLerp);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_camTarget.rotation, m_rLerp);
    }
}