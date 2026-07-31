using UnityEngine;

public interface IHealth
{
    public void TakeDamage(IDamage damage);
    public bool Dead();
}
