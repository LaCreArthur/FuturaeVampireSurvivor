using System;
using DG.Tweening;
using UnityEngine;

/// <summary>
///     Generic Health system that can be attached to any GameObject that needs health.
/// </summary>
[RequireComponent(typeof(DeathBehavior))]
public class HealthSystem : MonoBehaviour
{
    [SerializeField] int defaultMaxHealth = 10;
    //todo: remove serialized field and make it private
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    DeathBehavior _deathBehavior;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnMaxHealthChanged;
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

    void Awake()
    {
        _deathBehavior = GetComponent<DeathBehavior>();
        MaxHealth = CurrentHealth = defaultMaxHealth;
    }

    public void InitializeHealth(int maxHealth) => MaxHealth = CurrentHealth = maxHealth;

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        OnDamage?.Invoke();
        if (CurrentHealth <= 0)
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
