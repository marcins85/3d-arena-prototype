using UnityEngine;

public class PoisonDamage : DamageDecorator
{
    private int _tickDamage;
    public PoisonDamage(IDamage inner, int tickDamage) : base(inner) 
    { 
        _tickDamage = tickDamage;
    }
    public override int GetDamage()
    {
        return _inner.GetDamage() + _tickDamage;
    }
}
