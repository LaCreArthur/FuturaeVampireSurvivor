using System;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] bool invulnerability;
    [SerializeField] float invulnerabilityTime = 1f;

    float _invulnerabilityTimer;

    public static event Action<int> OnHealthChanged;
    public static event Action<int> OnMaxHealthChanged;
    public static event Action<bool> OnInvulnerabilityChanged;

    public override int CurrentHealth
    {
        get => currentHealth;
        protected set {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth);
        }
    }

    public override int MaxHealth
    {
        get => maxHealth;
        protected set {
            maxHealth = value;
            OnMaxHealthChanged?.Invoke(maxHealth);
        }
    }

    void Awake() => GameStateManager.OnPlaying += OnLevelStart;

    void Update()
    {
        if (invulnerability && _invulnerabilityTimer > 0)
        {
            _invulnerabilityTimer -= Time.deltaTime;
            if (_invulnerabilityTimer <= 0) invulnerability = false;
        }
    }

    void OnDestroy() => GameStateManager.OnPlaying -= OnLevelStart;
    void OnLevelStart() => CurrentHealth = MaxHealth = maxHealth;

    public override void TakeDamage(int amount)
    {
        if (invulnerability)
            return;
        base.TakeDamage(amount);
        if (CurrentHealth <= 0)
        {
            GameStateManager.SetState(GameState.GameOver);
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
