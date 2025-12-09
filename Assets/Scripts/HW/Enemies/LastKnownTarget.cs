using UnityEngine;

public class LastKnownTarget : MonoBehaviour
{
    public Vector3 Position { get; private set; }
    public bool HasPosition { get; private set; }

    public void UpdatePosition(Vector3 pos)
    {
        Position = pos;
        HasPosition = true;
    }

    public void Clear()
    {
        HasPosition = false;
    }
}
