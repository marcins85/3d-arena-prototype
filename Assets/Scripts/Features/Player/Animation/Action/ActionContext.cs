using UnityEngine;

public class ActionContext
{
    public Animator Animator;
    public IMovement Movement;
    public IDamage Damage;
    public bool Attack1Request;
    public bool Attack2Request;
    public bool HitRequest;
    public bool BlockRequest;
    public bool BlockHeld;

    public int ComboStep = 0;
    public bool QueuedAttack = false;

    public bool DefenceWindowOpen;

}
