using System;
using UnityEngine;

public class PlayerExperienceSystem : MonoBehaviour
{
    const float BASE_XP = 5;
    const float EXPONENT = 1.5f;

    public static event Action<float> OnExpChanged;
    public static event Action<int> OnLevelUp;

    public static int CurrentLevel { get; private set; }
    public static float ExperienceForNextLevel { get; private set; }
    public static float MultiplierBonus { get; private set; }
    public static float Exp { get; private set; }

    void Start() => GameStateManager.OnPlaying += OnLevelStart;
    void OnDestroy() => GameStateManager.OnPlaying -= OnLevelStart;

    static void OnLevelStart()
    {
        MultiplierBonus = 1;
        CurrentLevel = 1;
        ExperienceForNextLevel = GetExperienceForNextLevel();
    }

    public static void CollectExperience(int exp)
    {
        Debug.Log($"collected {exp} exp");
        Exp += exp * MultiplierBonus;
        OnExpChanged?.Invoke(Exp);
        if (Exp >= ExperienceForNextLevel)
        {
            Exp -= ExperienceForNextLevel;
            LevelUp();
        }
    }

    static void LevelUp()
    {
        CurrentLevel++;
        ExperienceForNextLevel = GetExperienceForNextLevel();
        OnLevelUp?.Invoke(CurrentLevel);
        Debug.Log($"Level up to {CurrentLevel} - {ExperienceForNextLevel} XP for next level");
    }

    static int GetExperienceForNextLevel() => Mathf.CeilToInt(BASE_XP * Mathf.Pow(CurrentLevel, EXPONENT));
}
