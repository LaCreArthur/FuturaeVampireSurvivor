using UnityEngine;

/// <summary>
///     DeathBehavior is an abstract class that defines a method Die that must be implemented by its subclasses.
///     Since it doesn't need to be swapped at runtime, it doesn't need to be a ScriptableObject.
///     And it is also required by the HealthSystem, so it must be a MonoBehaviour and we can use RequireComponent to
///     enforce it.
/// </summary>
public abstract class DeathBehavior : MonoBehaviour
{
    public abstract void Die(GameObject owner);
}
