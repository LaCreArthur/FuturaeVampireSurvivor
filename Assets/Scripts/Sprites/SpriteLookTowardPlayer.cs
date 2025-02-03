using ScriptableVariables;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookTowardPlayer : SpriteLookTo
{
    [SerializeField] TransformVar playerTransformVar;
    Transform _playerTransform;
    void Start() => _playerTransform = playerTransformVar.Value;
    protected override void SetLookingLeft() => lookingLeft = transform.position.x > _playerTransform.position.x;
}
