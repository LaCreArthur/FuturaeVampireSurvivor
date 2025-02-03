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

    [SerializeField] bool invulnerability;
    [SerializeField] float invulnerabilityTime = 1f;

    float _invulnerabilityTimer;

    DeathBehavior _deathBehavior;

    public event Action<int> OnHealthChanged;
    public event Action<int> OnMaxHealthChanged;
    public event Action<bool> OnInvulnerabilityChanged;
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
        GameStateManager.OnPlaying += OnLevelStart;
    }

    void Update()
    {
        if (invulnerability && _invulnerabilityTimer > 0)
        {
            _invulnerabilityTimer -= Time.deltaTime;
            if (_invulnerabilityTimer <= 0) invulnerability = false;
        }
    }

    void OnDestroy() => GameStateManager.OnPlaying -= OnLevelStart;
    public void OnSpawn() => CurrentHealth = MaxHealth;
    void OnLevelStart() => CurrentHealth = MaxHealth = maxHealth;

    public void TakeDamage(int amount)
    {
        if (invulnerability)
            return;

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
        }
    }

    public void SetInvulnerability(bool value)
    {
        invulnerability = value;
        OnInvulnerabilityChanged?.Invoke(invulnerability);
        if (invulnerability)
        {
            _invulnerabilityTimer = invulnerabilityTime;
        }
    }
}
