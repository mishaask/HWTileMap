using UnityEngine;

// Tracks how long the enemy has been chasing and how long it rests.
 
public class ChaseFatigue : MonoBehaviour
{
    [Header("Fatigue Settings")]
    [Tooltip("How many seconds of continuous chasing before the enemy gets tired.")]
    [SerializeField] private float runTimeBeforeTired = 5f;   // your default=5

    [Tooltip("How long the enemy rests when tired (in seconds).")]
    [SerializeField] private float restDuration = 3f;         // your '3 steps' => 3 sec

    private ChaserNew chaser;

    private float chaseTimer = 0f;
    private float restTimer = 0f;
    private bool isResting = false;

    public bool ShouldGoTired => !isResting && chaseTimer >= runTimeBeforeTired;
    public bool RestFinished => isResting && restTimer <= 0f;

    private void Awake()
    {
        chaser = GetComponent<ChaserNew>();
    }

    private void Update()
    {
        // Count time only while actually chasing
        if (chaser != null && chaser.enabled && !isResting)
        {
            chaseTimer += Time.deltaTime;
        }

        if (isResting)
        {
            restTimer -= Time.deltaTime;
        }
    }

    public void StartRest()
    {
        isResting = true;
        restTimer = restDuration;
        chaseTimer = 0f;
    }

    public void FinishRest()
    {
        isResting = false;
    }
}
