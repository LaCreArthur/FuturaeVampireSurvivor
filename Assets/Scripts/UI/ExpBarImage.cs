using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ExpBarImage : MonoBehaviour
{
    Image _image;
    float _experienceForNextLevel = 1; // set to non-zero to avoid division by zero

    void Awake() => _image = GetComponent<Image>();
    void Start()
    {
        PlayerExperienceSystem.OnExpChanged += UpdateLifeBar;
        PlayerExperienceSystem.OnLevelUp += OnLevelUp;
        OnLevelUp(PlayerExperienceSystem.CurrentLevel);
        UpdateLifeBar(PlayerExperienceSystem.Exp);
    }

    void OnDestroy()
    {
        PlayerExperienceSystem.OnExpChanged -= UpdateLifeBar;
        PlayerExperienceSystem.OnLevelUp -= OnLevelUp;
    }

    void OnLevelUp(int level)
    {
        _experienceForNextLevel = PlayerExperienceSystem.ExperienceForNextLevel;
        UpdateLifeBar(PlayerExperienceSystem.Exp);
    }
    void UpdateLifeBar(float exp) => _image.fillAmount = exp / _experienceForNextLevel;
}
