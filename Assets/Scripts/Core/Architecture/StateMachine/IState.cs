public interface IState
{
    void Enter();
    void Exit();
    void Update();
    void OnAnimationEvent(string evt);
}