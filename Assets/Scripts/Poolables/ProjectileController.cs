using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    float _moveSpeed;
    float _damage;
    GameObject _attacker;

    void Update() => transform.position += transform.forward * (_moveSpeed * Time.deltaTime);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_attacker.tag)) return; // Ignore collisions with the attacker
        var otherHealth = other.GetComponent<HealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(_damage); // Deal damage to the target
            PoolManager.Despawn(gameObject); // Despawn the projectile
        }
    }

    public void Initialize(float speed, float damage, GameObject attacker)
    {
        _moveSpeed = speed;
        _damage = damage;
        _attacker = attacker;
    }
}
