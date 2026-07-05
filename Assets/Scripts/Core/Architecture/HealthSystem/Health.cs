using UnityEngine;

public class Health : IHealth
{
    private int _health;
    public Health(int health)
    {
        _health = health;
    }

    public bool Dead()
    {
        return _health <= 0;
    }

    public void TakeDamage(IDamage damage)
    {
        _health -= damage.GetDamage();
        if (Dead()) _health = 0;
    }
}
