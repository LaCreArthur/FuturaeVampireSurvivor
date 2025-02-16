using UnityEngine;

public class KnifeProjectileController : MonoBehaviour
{
    float _moveSpeed;
    int _damage;
    float _knockback;
    GameObject _attacker;

    void Update() => transform.position += transform.up * (_moveSpeed * Time.deltaTime);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_attacker.tag)) return; // Ignore collisions with the attacker
        // using .attachedRigidbody because the collider can be on a child object of the target
        var otherHealth = other.attachedRigidbody?.GetComponent<EnemyHealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(_damage, _knockback); // Deal damage to the target
            PoolManager.Despawn(gameObject); // Despawn the projectile
        }
    }

    public void Initialize(float speed, int damage, float knockback, GameObject attacker)
    {
        _moveSpeed = speed;
        _damage = damage;
        _knockback = knockback;
        _attacker = attacker;
    }
}
