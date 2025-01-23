using UnityEngine;

[CreateAssetMenu(fileName = "MoveTowardPlayer", menuName = "Move Behaviors/MoveTowardPlayer")]
public class MoveTowardPlayerSO : MoveBehaviorSO
{
    public override void Move(Transform ownerTransform)
    {
        if (PlayerController.PlayerTransform == null) return;

        Vector3 playerPos = PlayerController.PlayerTransform.position;

        if (Vector3.SqrMagnitude(ownerTransform.position - playerPos) < 0.2f) return;

        ownerTransform.position = Vector3.MoveTowards(
            ownerTransform.position,
            playerPos,
            speed * Time.deltaTime
        );
    }
}
