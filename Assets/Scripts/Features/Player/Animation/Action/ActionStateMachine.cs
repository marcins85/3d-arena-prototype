using UnityEngine;

public class ActionStateMachine : StateMachine
{
    public IState Attack;
    public IState Block;
    public IState Hit;

    public ActionStateMachine(ActionContext ctx)
    {
        Attack = new AttackState(ctx, this);
        Block = new BlockState(ctx, this);
        Hit = new HitState(ctx, this);
    }
}
