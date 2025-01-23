using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(DeathBehavior))]
public class HealthSystem : MonoBehaviour
{
    [SerializeField] float defaultMaxHealth = 10;

    DeathBehavior _deathBehavior;
    float _maxHealth;
    float _currentHealth;

    //todo: will be used for any component that wants to know when the object dies (ex: spawn xp or a particle effect)
    public event Action OnDeath;

    public float MaxHealth
    {
        get => _maxHealth;
        set {
            _maxHealth = value;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
    }

    void Awake()
    {
        _deathBehavior = GetComponent<DeathBehavior>();
        MaxHealth = _currentHealth = defaultMaxHealth;
    }

    public void InitializeHealth(float maxHealth) => MaxHealth = _currentHealth = maxHealth;

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            _deathBehavior.Die(gameObject);
            OnDeath?.Invoke();
        }
        else
        {
            transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
        }
    }
}
