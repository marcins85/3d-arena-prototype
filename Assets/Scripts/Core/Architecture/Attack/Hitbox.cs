using UnityEngine;

public class Hitbox : MonoBehaviour, IHitbox
{
    private Collider[] hits;

    [SerializeField] private LayerMask _oponentLayer;
    public void Activate(IDamage damage)
    {
        foreach (var hit in hits)
        {
            var health = hit.GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void Update()
    {
        hits = Physics.OverlapSphere(transform.position, 2f, _oponentLayer);
    }
}
