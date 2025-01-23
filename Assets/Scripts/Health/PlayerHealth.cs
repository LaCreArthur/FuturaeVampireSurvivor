using System;
using UnityEngine;

/// <summary>
///     Because player health need unique specific events, we create a separate class to handle them.
///     We just relay the events from the HealthSystem.
/// </summary>
[RequireComponent(typeof(HealthSystem))]
public class PlayerHealth : MonoBehaviour
{
    public static int MaxHealth;
    public static int CurrentHealth;
    HealthSystem _healthSystem;

    public static event Action<int> OnPlayerHealthChanged;
    public static event Action<int> OnPlayerMaxHealthChanged;
    public static event Action OnPlayerDamage;
    public static event Action OnPlayerDeath;

    void Awake() => _healthSystem = GetComponent<HealthSystem>();

    void OnEnable()
    {
        _healthSystem.OnHealthChanged += OnHealthChanged;
        _healthSystem.OnMaxHealthChanged += OnMaxHealthChanged;
        _healthSystem.OnDamage += OnDamage;
        _healthSystem.OnDeath += OnDeath;
    }

    void OnDisable()
    {
        _healthSystem.OnHealthChanged -= OnHealthChanged;
        _healthSystem.OnMaxHealthChanged -= OnMaxHealthChanged;
        _healthSystem.OnDamage -= OnDamage;
        _healthSystem.OnDeath -= OnDeath;
    }

    static void OnHealthChanged(int health)
    {
        CurrentHealth = health;
        OnPlayerHealthChanged?.Invoke(health);
    }

    static void OnMaxHealthChanged(int maxHealth)
    {
        MaxHealth = maxHealth;
        OnPlayerMaxHealthChanged?.Invoke(maxHealth);
    }

    static void OnDamage() => OnPlayerDamage?.Invoke();
    static void OnDeath() => OnPlayerDeath?.Invoke();
}
