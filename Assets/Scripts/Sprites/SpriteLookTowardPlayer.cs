using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookTowardPlayer : SpriteLookTo
{
    Transform _playerTransform;
    void Start() => _playerTransform = PlayerSingleton.Transform;
    protected override void SetLookingLeft() => lookingLeft = transform.position.x > _playerTransform.position.x;
}
