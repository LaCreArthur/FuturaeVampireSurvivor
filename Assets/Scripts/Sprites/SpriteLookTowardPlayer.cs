using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookTowardPlayer : SpriteLookTo
{
    Transform _player;
    void Start() => _player = PlayerStaticReferences.PlayerTransform;
    protected override void SetLookingLeft() => lookingLeft = transform.position.x > _player.position.x;
}
