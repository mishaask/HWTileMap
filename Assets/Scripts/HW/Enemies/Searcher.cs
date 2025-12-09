using UnityEngine;

// Go to the last known player position, then rotate/search for a short time.

[RequireComponent(typeof(LastKnownTarget))]
public class Searcher : TargetMover
{
    [SerializeField] private float searchDuration = 2f;

    private LastKnownTarget memory;
    private Rotator rotator;
    private float remainingSearchTime;
    private bool reachedSpot;

    public bool DoneSearching => reachedSpot && remainingSearchTime <= 0f;

    protected override void Start()
    {
        base.Start();
        memory = GetComponent<LastKnownTarget>();
        rotator = GetComponent<Rotator>();
    }

    private void OnEnable()
    {
        reachedSpot = false;
        remainingSearchTime = searchDuration;
        if (rotator != null)
            rotator.enabled = false;   // only rotate after we reach the spot
    }

    private void OnDisable()
    {
        // stop rotation when leaving search state
        if (rotator != null)
            rotator.enabled = false;
    }

    private void Update()
    {
        if (!reachedSpot)
        {
            if (memory.HasPosition)
                SetTarget(memory.Position);

            if (atTarget)
            {
                reachedSpot = true;
                if (rotator != null)
                    rotator.enabled = true; // look around
            }
        }
        else
        {
            remainingSearchTime -= Time.deltaTime;
        }
    }
}
