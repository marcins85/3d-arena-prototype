using UnityEngine;

public class EnemyAttack : IAttack
{
    [SerializeField] private IHitbox _hitbox;

    public EnemyAttack(IHitbox hitbox)
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
        IDamage damage = new NormalDamage(30);

        // if (hasPoison)
        //     damage = new PoisonDamage(damage, 5);

        // if (doubleDamage)
        //     damage = new DoubleDamage(damage);

        return damage;
    }
}
