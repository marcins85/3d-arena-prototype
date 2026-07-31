using UnityEngine;

public class PlayerAttack : IAttack
{
    [SerializeField] private IHitbox _hitbox;

    public PlayerAttack(IHitbox hitbox)
    {
        _hitbox = hitbox;
    }

    public void PerformAttack()
    {
        IDamage damage = BuildDamage();

        _hitbox.Activate(damage);
    }

    private IDamage BuildDamage()
    {
        IDamage damage = new NormalDamage(50);

        // if (hasPoison)
        //     damage = new PoisonDamage(damage, 5);

        // if (doubleDamage)
        //     damage = new DoubleDamage(damage);

        return damage;
    }
}
