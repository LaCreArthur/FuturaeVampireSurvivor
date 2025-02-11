using System;
using ScriptableVariables;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    const float BASE_XP = 5;
    const float EXPONENT = 1.5f;

    [SerializeField] IntVar currentLevel;
    [SerializeField] FloatVar playerExp;
    [SerializeField] FloatVar playerMaxExp;

    public static event Action<int> OnLevelUp;
    public static event Action<float> OnExpChanged;
    public static event Action<float> OnMaxExpChanged;

    int CurrentLevel
    {
        get => currentLevel.Value;
        set {
            currentLevel.Value = value;
            if (value > 1) OnLevelUp?.Invoke(value);
            MaxExp = Mathf.CeilToInt(BASE_XP * Mathf.Pow(value, EXPONENT));
            Debug.Log($"Level up to {value} - {MaxExp} XP for next level");
            Exp = 0;
        }
    }
    float MaxExp
    {
        get => playerMaxExp.Value;
        set {
            playerMaxExp.Value = value;
            OnMaxExpChanged?.Invoke(value);
        }
    }
    float MultiplierBonus { get; set; }
    float Exp
    {
        get => playerExp.Value;
        set {
            playerExp.Value = value;
            OnExpChanged?.Invoke(value);
            if (value >= MaxExp)
                CurrentLevel++;
        }
    }

    void Start()
    {
        GameStateManager.OnPlaying += OnLevelStart;
        ExperienceOrb.OnExpCollected += CollectExperience;
        CharacterSelector.CharacterChanged += OnCharacterChanged;
    }

    void OnDestroy()
    {
        GameStateManager.OnPlaying -= OnLevelStart;
        ExperienceOrb.OnExpCollected -= CollectExperience;
        CharacterSelector.CharacterChanged -= OnCharacterChanged;
    }

    void OnCharacterChanged(CharacterSO characterSO) => MultiplierBonus = 1 + characterSO.baseStats.growth / 100f;

    void OnLevelStart()
    {
        MultiplierBonus = 1;
        CurrentLevel = 1;
    }

    void CollectExperience(int exp) => Exp += exp * MultiplierBonus;
}
