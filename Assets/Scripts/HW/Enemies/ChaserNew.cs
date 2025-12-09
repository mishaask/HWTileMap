using UnityEngine;

public class ChaserNew : TargetMover
{
    [Tooltip("The object that we try to chase")]
    [SerializeField] Transform targetObject = null;

    private LastKnownTarget memory;

    private void Awake()
    {
        memory = GetComponent<LastKnownTarget>();
    }

    public Vector3 TargetObjectPosition()
    {
        return targetObject.position;
    }

    private void Update()
    {
        Vector3 pos = targetObject.position;
        SetTarget(pos);
        if (memory != null)
            memory.UpdatePosition(pos);
    }
}
