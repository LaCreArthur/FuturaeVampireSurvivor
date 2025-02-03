using UnityEngine;

public class EnemyAttackBehavior : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float attackRate;

    float _attackTimer;
    bool _canAttack;

    void Update()
    {
        if (_canAttack) return;

        _attackTimer -= Time.deltaTime;
        if (_attackTimer <= 0)
        {
            _attackTimer = attackRate;
            _canAttack = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_canAttack) return;

        if (other.gameObject.CompareTag(transform.tag)) return; // Ignore collisions with self and other enemies

        var otherHealth = other.rigidbody?.GetComponent<HealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
            _canAttack = false;
        }
    }
}
