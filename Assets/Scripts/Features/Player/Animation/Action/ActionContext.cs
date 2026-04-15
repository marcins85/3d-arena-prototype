using UnityEngine;

public class ActionContext
{
    public Animator Animator;
    public bool Attack1Request;
    public bool Attack2Request;

    public int ComboStep = 0;
    public bool QueuedAttack = false;

}
