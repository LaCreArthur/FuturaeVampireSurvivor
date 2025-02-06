using System;
using ScriptableVariables;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] bool invulnerability;
    [SerializeField] float invulnerabilityTime = 1f;

    [SerializeField] FloatVar playerHp;
    [SerializeField] FloatVar playerMaxHp;

    float _invulnerabilityTimer;

    public static event Action<bool> OnInvulnerabilityChanged;

    void Awake()
    {
        GameStateManager.OnPlaying += OnLevelStart;
        HpChanged += OnHpChanged;
        MaxHpChanged += OnMaxHpChanged;
    }

    void Update()
    {
        if (invulnerability && _invulnerabilityTimer > 0)
        {
            _invulnerabilityTimer -= Time.deltaTime;
            if (_invulnerabilityTimer <= 0) invulnerability = false;
        }
    }

    void OnDestroy()
    {
        GameStateManager.OnPlaying -= OnLevelStart;
        HpChanged -= OnHpChanged;
        MaxHpChanged -= OnMaxHpChanged;
    }

    void OnHpChanged(float hp) => playerHp.Value = hp;
    void OnMaxHpChanged(float maxHp) => playerMaxHp.Value = maxHp;
    void OnLevelStart() => CurrentHp = MaxHp;

    public override void TakeDamage(int amount)
    {
        if (invulnerability)
            return;
        base.TakeDamage(amount);
        if (CurrentHp <= 0)
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
