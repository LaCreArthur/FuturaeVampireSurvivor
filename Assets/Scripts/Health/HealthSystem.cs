using System;
using DG.Tweening;
using UnityEngine;

/// <summary>
///     Generic Health system that can be attached to any GameObject that needs health.
/// </summary>
[RequireComponent(typeof(DeathBehavior))]
public class HealthSystem : MonoBehaviour, IPoolable
{
    [SerializeField] int maxHealth;
    [SerializeField] [ReadOnly] int currentHealth;

    [SerializeField] bool hasGracePeriod;
    [SerializeField] float gracePeriod = 1f;

    bool _isInGracePeriod;
    float _graceTimer;

    DeathBehavior _deathBehavior;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnMaxHealthChanged;
    public event Action<bool> OnGracePeriodStatusChanged;
    public event Action OnDamage;
    public event Action OnDeath;

    public int CurrentHealth
    {
        get => currentHealth;
        private set {
            currentHealth = value;
            OnHealthChanged?.Invoke(currentHealth);
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set {
            maxHealth = value;
            OnMaxHealthChanged?.Invoke(maxHealth);
            if (CurrentHealth > maxHealth)
            {
                CurrentHealth = maxHealth;
            }
        }
    }
    public bool IsInGracePeriod
    {
        get => _isInGracePeriod;
        set {
            _isInGracePeriod = value;
            OnGracePeriodStatusChanged?.Invoke(_isInGracePeriod);
        }
    }

    void Awake()
    {
        _deathBehavior = GetComponent<DeathBehavior>();
        CurrentHealth = MaxHealth = maxHealth;
    }

    void Update()
    {
        if (hasGracePeriod && _graceTimer > 0)
        {
            _graceTimer -= Time.deltaTime;
            if (_graceTimer <= 0) IsInGracePeriod = false;
        }
    }

    public void OnSpawn() => CurrentHealth = MaxHealth;

    public void TakeDamage(int amount)
    {
        if (hasGracePeriod && IsInGracePeriod)
        {
            return;
        }

        CurrentHealth -= amount;
        OnDamage?.Invoke();
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
            _deathBehavior.OnDeath();
        }
        else
        {
            transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
            if (hasGracePeriod)
            {
                _graceTimer = gracePeriod;
                IsInGracePeriod = true;
            }
        }
    }
}
