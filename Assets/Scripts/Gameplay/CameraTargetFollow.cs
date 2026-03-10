using UnityEngine;

public class CamTargetFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;

    void LateUpdate()
    {
        transform.position = _player.position + _offset;
    }
}