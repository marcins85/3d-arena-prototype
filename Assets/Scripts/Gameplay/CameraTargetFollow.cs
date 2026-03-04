using UnityEngine;

public class CamTargetFollow : MonoBehaviour
{
    [SerializeField] private Transform m_player;
    [SerializeField] private Vector3 m_offset;

    void LateUpdate()
    {
        transform.position = m_player.position + m_offset;
    }
}