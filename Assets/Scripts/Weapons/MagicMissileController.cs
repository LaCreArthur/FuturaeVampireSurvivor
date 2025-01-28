using UnityEngine;

public class MagicMissileController : MonoBehaviour
{
    float _moveSpeed;
    int _damage;
    GameObject _attacker;
    Vector3 _direction;

    void Update() => transform.position += _direction * (_moveSpeed * Time.deltaTime);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_attacker.tag)) return; // Ignore collisions with the attacker
        // using .attachedRigidbody because the collider can be on a child object of the target
        var otherHealth = other.attachedRigidbody?.GetComponent<HealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(_damage); // Deal damage to the target
            PoolManager.Despawn(gameObject); // Despawn the projectile
        }
    }

    public void Initialize(float speed, int damage, GameObject attacker, Vector3 direction)
    {
        _moveSpeed = speed;
        _damage = damage;
        _attacker = attacker;
        _direction = direction;
    }
}
