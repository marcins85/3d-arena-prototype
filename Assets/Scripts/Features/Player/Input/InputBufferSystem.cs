using UnityEngine;

public class InputBufferSystem : IInputBuffer
{
    private Vector2 _moveBuffer;
    private bool _hasMoveBuffer;

    public void BufferMove(Vector2 input)
    {
        _moveBuffer = input;
        _hasMoveBuffer = true;
    }

    public bool TryConsumeMove(out Vector2 input)
    {
        if (_hasMoveBuffer)
        {
            input = _moveBuffer;
            _hasMoveBuffer = false;
            return true;
        }

        input = Vector2.zero;
        return false;
    }

    public void ClearMove()
    {
        _moveBuffer = Vector2.zero;
        _hasMoveBuffer = false;
    }
}
