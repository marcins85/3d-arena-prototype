using UnityEngine;

public class DoubleDamage : DamageDecorator
{
    public DoubleDamage(IDamage inner) : base(inner) { }
    public override int GetDamage()
    {
        return _inner.GetDamage() * 2;
    }
}
