using UnityEngine;

public class MoveTowardPlayer : MoveBehavior
{
    // we cache the player's transform to avoid calling PlayerStaticReferences every frame
    Transform _playerTransform;

    public override void Initialize() => _playerTransform = PlayerStaticReferences.PlayerTransform;

    public override void Move()
    {
        if (_playerTransform == null) return;
        Vector3 playerPos = _playerTransform.position;

        if (Vector3.SqrMagnitude(transform.position - playerPos) < 0.2f) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            playerPos,
            speed * Time.deltaTime);
    }
}
