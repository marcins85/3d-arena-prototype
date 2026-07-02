using UnityEngine;

public class Hitbox : MonoBehaviour, IHitbox
{
    private Collider[] hits;

    [SerializeField] private LayerMask _playerLayer;
    public void Activate(IDamage damage)
    {
        foreach (var hit in hits)
        {
            Debug.Log(hit.tag + ": " + damage.GetDamage());
        }
    }

    private void Update()
    {
        hits = Physics.OverlapSphere(transform.position, 2f, _playerLayer);
    }
}
