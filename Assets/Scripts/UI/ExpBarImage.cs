public class ExpBarImage : BarImage
{
    void Start()
    {
        PlayerExperienceSystem.OnExpChanged += SetValue;
        PlayerExperienceSystem.OnLevelUp += OnLevelUp;
        OnLevelUp(PlayerExperienceSystem.CurrentLevel);
    }

    void OnDestroy()
    {
        PlayerExperienceSystem.OnExpChanged -= SetValue;
        PlayerExperienceSystem.OnLevelUp -= OnLevelUp;
    }

    void OnLevelUp(int _) // _ is a placeholder for the unused parameter, we don't need the level in this method
    {
        SetMaxValue(PlayerExperienceSystem.ExperienceForNextLevel);
        SetValue(PlayerExperienceSystem.Exp);
    }
}
