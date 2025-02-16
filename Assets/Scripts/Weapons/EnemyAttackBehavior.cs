using UnityEngine;

public class EnemyAttackBehavior : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float attackRate;

    int _baseDamage;
    float _attackTimer;
    bool _canAttack;

    void Awake()
    {
        _baseDamage = damage;
        _attackTimer = attackRate;
    }

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

    void OnCollisionEnter2D(Collision2D other) => CheckCollision(other);

    // We also want to check for collisions while the player is still in contact with the enemy
    void OnCollisionStay2D(Collision2D other) => CheckCollision(other);

    void CheckCollision(Collision2D other)
    {
        if (!_canAttack) return;

        if (other.gameObject.CompareTag(transform.tag)) return;

        var otherHealth = other.rigidbody?.GetComponent<HealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
            _canAttack = false;
        }
    }

    public void IncreaseDamage(float percentage) => damage = (int)(_baseDamage + _baseDamage * percentage);
}
