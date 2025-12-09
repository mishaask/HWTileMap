using UnityEngine;

/**
 * Tired state: enemy stands still and rests.
 */
[RequireComponent(typeof(ChaseFatigue))]
public class Tired : MonoBehaviour
{
    private ChaseFatigue fatigue;

    private void Awake()
    {
        fatigue = GetComponent<ChaseFatigue>();
    }

    private void OnEnable()
    {
        // When we enter the tired state, start resting.
        fatigue.StartRest();
    }

    private void OnDisable()
    {
        // When we leave the tired state, stop resting.
        fatigue.FinishRest();
    }

    private void Update()
    {
        // No movement here. The FSM decides when to leave this state
        // based on fatigue.RestFinished.
    }
}
