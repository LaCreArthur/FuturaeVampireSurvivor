using UnityEngine;

[CreateAssetMenu(fileName = "WanderMove", menuName = "Move Behaviors/WanderMove")]
public class WanderMoveSO : MoveBehaviorSO
{
    [SerializeField] float nextMoveCooldown;
    [SerializeField] float nextMoveRadius;

    public override void Initialize(GameObject owner)
    {
        var moveController = owner.GetComponent<WanderMoveController>();
        if (moveController == null) moveController = owner.AddComponent<WanderMoveController>();
        moveController.Initialize(speed, nextMoveCooldown, nextMoveRadius);
    }

    // In this behavior, the move behavior is handled by the WanderMoveController because it is instance-specific
    // Not sure of this pattern yet, but it is a way to handle instance-specific data
    public override void Move(Transform ownerTransform) {}
}
