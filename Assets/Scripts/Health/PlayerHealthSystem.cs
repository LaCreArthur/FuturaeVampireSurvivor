using ScriptableVariables;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField] FloatVar playerHp;
    [SerializeField] FloatVar playerMaxHp;

    int _armor;
    float _recovery;

    void Awake()
    {
        GameStateManager.OnPlaying += OnLevelStart;
        HpChanged += OnHpChanged;
        MaxHpChanged += OnMaxHpChanged;
        CharacterSelector.CharacterChanged += OnCharacterChanged;
        ModifierSystem.CharacterModifiersUpdated += OnModifiersUpdated;
    }

    void Update()
    {
        if (CurrentHp < MaxHp && _recovery > 0)
        {
            CurrentHp += _recovery * Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        GameStateManager.OnPlaying -= OnLevelStart;
        HpChanged -= OnHpChanged;
        MaxHpChanged -= OnMaxHpChanged;
        CharacterSelector.CharacterChanged -= OnCharacterChanged;
        ModifierSystem.CharacterModifiersUpdated -= OnModifiersUpdated;
    }

    void OnModifiersUpdated(Modifiers stats)
    {
        MaxHp = stats.maxHealth;
        CurrentHp = Mathf.Min(CurrentHp, MaxHp);
        _armor = stats.armor;
        _recovery = stats.recovery;
    }

    void OnCharacterChanged(CharacterSO characterSO)
    {
        maxHp = characterSO.baseStats.maxHealth;
        CurrentHp = MaxHp = maxHp;
    }

    void OnHpChanged(float hp) => playerHp.Value = hp;
    void OnMaxHpChanged(float newMaxHp) => playerMaxHp.Value = newMaxHp;
    void OnLevelStart() => CurrentHp = MaxHp = maxHp;

    public override void TakeDamage(int amount)
    {
        int realDamage = amount - _armor;
        base.TakeDamage(amount);
        if (CurrentHp <= 0)
        {
            GameStateManager.SetState(GameState.GameOver);
        }
    }
}
