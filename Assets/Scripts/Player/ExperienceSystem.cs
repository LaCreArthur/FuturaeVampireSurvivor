using System;
using UnityEngine;

public class ExperienceSystem : MonoBehaviour
{
    const float BASE_XP = 5;
    const float EXPONENT = 1.5f;
    [SerializeField] int _currentLevel;
    float _exp;
    float _maxExp;
    public static event Action<int> OnLevelUp;

    public event Action<float> OnExpChanged;
    public event Action<float> OnMaxExpChanged;

    int CurrentLevel
    {
        get => _currentLevel;
        set {
            _currentLevel = value;
            OnLevelUp?.Invoke(_currentLevel);
            MaxExp = Mathf.CeilToInt(BASE_XP * Mathf.Pow(CurrentLevel, EXPONENT));
            Debug.Log($"Level up to {CurrentLevel} - {MaxExp} XP for next level");
            Exp = 0;
        }
    }
    public float MaxExp
    {
        get => _maxExp;
        private set {
            _maxExp = value;
            OnMaxExpChanged?.Invoke(_maxExp);
        }
    }
    float MultiplierBonus { get; set; }
    public float Exp
    {
        get => _exp;
        private set {
            _exp = value;
            OnExpChanged?.Invoke(_exp);
            Debug.Log($"collected {_exp} exp, remaining {MaxExp - Exp}, (total exp for next {MaxExp})");
            if (_exp >= MaxExp)
                CurrentLevel++;
        }
    }

    void Start()
    {
        GameStateManager.OnPlaying += OnLevelStart;
        ExperienceOrb.OnExpCollected += CollectExperience;
    }

    void OnDestroy()
    {
        GameStateManager.OnPlaying -= OnLevelStart;
        ExperienceOrb.OnExpCollected -= CollectExperience;
    }
    void CollectExperience(int exp) => Exp += exp * MultiplierBonus;

    void OnLevelStart()
    {
        MultiplierBonus = 1;
        CurrentLevel = 1;
    }
}
