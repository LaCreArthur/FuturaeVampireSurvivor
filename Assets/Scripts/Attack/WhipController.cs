using DG.Tweening;
using UnityEngine;

public class WhipController : MonoBehaviour
{
    GameObject _attacker;
    Vector3 _initialScale;
    int _damage;
    float _cooldown;

    void Awake() => _initialScale = transform.localScale;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_attacker.tag)) return; // Ignore collisions with the attacker
        // using .attachedRigidbody because the collider can be on a child object of the target
        var otherHealth = other.attachedRigidbody?.GetComponent<HealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(_damage); // Deal damage to the target
        }
    }

    public void Initialize(int damage, float cooldown, GameObject attacker)
    {
        _damage = damage;
        _attacker = attacker;
        _cooldown = cooldown;
        transform.DOScale(_initialScale * 1.5f, _cooldown / 2f).SetEase(Ease.OutBounce);
        transform.DOPunchPosition(Vector3.forward * 0.1f, 3f * _cooldown / 4f).SetRelative(true).OnComplete(() => PoolManager.Despawn(gameObject));
    }
}
