using UnityEngine;

/// <summary>
///     MoveBehaviorSO is a scriptable object because it has configurable data,
///     and it could be swapped behaviors at runtime,
///     and it can be shared across multiple entities
/// </summary>
public abstract class MoveBehaviorSO : ScriptableObject
{
    public float speed = 5f;
    public abstract void Move(Transform ownerTransform);
    public virtual void Initialize(GameObject owner) {}
}
