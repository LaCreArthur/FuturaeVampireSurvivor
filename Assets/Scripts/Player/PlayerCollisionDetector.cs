using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class PlayerCollisionDetector : MonoBehaviour
{
    HealthSystem _healthSystem;
    void Awake() => _healthSystem = GetComponent<HealthSystem>();

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out DamageOnCollision damageOnCollision))
        {
            _healthSystem.TakeDamage(damageOnCollision.Damage);
        }
    }
}
