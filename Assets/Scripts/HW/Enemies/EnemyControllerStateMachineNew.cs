using UnityEngine;

// Patroller -> Chaser -> Tired -> Searcher -> Patroller

[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(ChaserNew))]
[RequireComponent(typeof(Searcher))]
[RequireComponent(typeof(LastKnownTarget))]
[RequireComponent(typeof(ChaseFatigue))]
[RequireComponent(typeof(Tired))]
public class EnemyControllerStateMachineNew : StateMachine
{
    [SerializeField] float radiusToWatch = 5f;

    private Patroller patroller;
    private ChaserNew chaser;
    private Searcher searcher;
    private Tired tired;
    private ChaseFatigue fatigue;
    private LastKnownTarget memory;

    private float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, chaser.TargetObjectPosition());
    }

    private void Awake()
    {
        chaser = GetComponent<ChaserNew>();
        patroller = GetComponent<Patroller>();
        searcher = GetComponent<Searcher>();
        tired = GetComponent<Tired>();
        memory = GetComponent<LastKnownTarget>();
        fatigue = GetComponent<ChaseFatigue>();

        base
            .AddState(patroller)   // INITIAL state
            .AddState(chaser)
            .AddState(searcher)
            .AddState(tired)

            // CHASING LOGIC

            // Patrol → Chaser when player enters vision
            .AddTransition(patroller, () => DistanceToTarget() <= radiusToWatch, chaser)

            // Chaser → Tired (fatigue reached)
            .AddTransition(chaser, () => fatigue.ShouldGoTired, tired)

            // Chaser → Searcher (lost player before tired)
            .AddTransition(chaser, () =>
                DistanceToTarget() > radiusToWatch && !fatigue.ShouldGoTired,
                searcher)


            // TIRED LOGIC

            // Tired → Chaser (rest done & player visible)
            .AddTransition(tired, () =>
                fatigue.RestFinished && DistanceToTarget() <= radiusToWatch,
                chaser)

            // Tired → Searcher (rest done & player not visible but memory exists)
            .AddTransition(tired, () =>
                fatigue.RestFinished && DistanceToTarget() > radiusToWatch && memory.HasPosition,
                searcher)

            // Tired → Patrol (rest done & no memory)
            .AddTransition(tired, () =>
                fatigue.RestFinished && DistanceToTarget() > radiusToWatch && !memory.HasPosition,
                patroller)


            // SEARCH LOGIC

            // Searcher → Patrol when done scanning / reached last known point
            .AddTransition(searcher, () => searcher.DoneSearching, patroller);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
