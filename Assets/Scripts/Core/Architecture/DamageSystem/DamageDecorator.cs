using UnityEngine;

public abstract class DamageDecorator : IDamage
{
    protected IDamage _inner;

    protected DamageDecorator(IDamage inner)
    {
        _inner = inner;
    }

    public abstract int GetDamage();
}
