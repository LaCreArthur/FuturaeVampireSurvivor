using ScriptableVariables;
using UnityEngine;

/// <summary>
///     This move behavior moves in the direction of the player but will not stop when it reaches the player.
/// </summary>
public class MoveThroughPlayer : MoveBehavior
{
    [SerializeField] TransformVar playerTransformVar;
    Transform _playerTransform;
    Vector3 _direction;

    public override void Initialize()
    {
        // Ensure that _playerTransform is set to the player's transform if it isn't already
        _playerTransform ??= playerTransformVar.Value;
        _direction = (_playerTransform.position - transform.position).normalized;
    }

    public override void Move() => transform.position += _direction * (speed * Time.deltaTime);
}
