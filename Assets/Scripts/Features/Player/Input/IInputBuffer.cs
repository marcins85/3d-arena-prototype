using UnityEngine;

public interface IInputBuffer
{
    void BufferMove(Vector2 input);
    bool TryConsumeMove(out Vector2 input);
    void ClearMove();
}
