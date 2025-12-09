using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeightedTargetMover))]
public class WeightedClickMover : MonoBehaviour
{
    [SerializeField] private InputAction moveTo = new InputAction(type: InputActionType.Button);
    [SerializeField]
    private InputAction moveToLocation =
        new InputAction(type: InputActionType.Value, expectedControlType: "Vector2");

    private WeightedTargetMover mover;

    private void OnValidate()
    {
        if (moveTo.bindings.Count == 0)
            moveTo.AddBinding("<Mouse>/leftButton");
        if (moveToLocation.bindings.Count == 0)
            moveToLocation.AddBinding("<Mouse>/position");
    }

    private void Awake()
    {
        mover = GetComponent<WeightedTargetMover>();
    }

    private void OnEnable()
    {
        moveTo.Enable();
        moveToLocation.Enable();
    }

    private void OnDisable()
    {
        moveTo.Disable();
        moveToLocation.Disable();
    }

    private void Update()
    {
        if (moveTo.WasPerformedThisFrame())
        {
            Vector2 screen = moveToLocation.ReadValue<Vector2>();
            Vector3 world = Camera.main.ScreenToWorldPoint(screen);
            world.z = 0f;
            mover.SetTarget(world);
        }
    }
}
