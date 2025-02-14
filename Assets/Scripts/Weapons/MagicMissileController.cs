using DG.Tweening;
using UnityEngine;

public class MagicMissileController : MonoBehaviour, IPoolable
{
    float _moveSpeed;
    int _damage;
    GameObject _attacker;
    Vector3 _direction;
    bool _hasHit;

    void Update()
    {
        if (_hasHit) return;
        transform.position += _direction * (_moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasHit || other.CompareTag(_attacker.tag)) return; // Ignore collisions with the attacker
        // using .attachedRigidbody because the collider can be on a child object of the target
        var otherHealth = other.attachedRigidbody?.GetComponent<HealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(_damage); // Deal damage to the target
            _hasHit = true;
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => PoolManager.Despawn(gameObject)); // Despawn the projectile
        }
    }

    public void OnSpawn()
    {
        _hasHit = false;
        transform.localScale = Vector3.one;
    }

    public void Initialize(float speed, int damage, GameObject attacker, Vector3 direction)
    {
        _moveSpeed = speed;
        _damage = damage;
        _attacker = attacker;
        _direction = direction;
    }
}
