using UnityEngine;

public class NormalDamage : IDamage
{
    private int _damage;

    public NormalDamage(int damage)
    {
        _damage = damage;
    }

    public int GetDamage()
    {
        return _damage;
    }
}
