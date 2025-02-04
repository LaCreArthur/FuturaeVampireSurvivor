using UnityEngine;

public class TimeManager : MonoBehaviour
{
    void Start()
    {
        GameStateManager.OnPlaying += ResumeTime;
        ButtonResume.OnResumeButtonClicked += ResumeTime;
        ButtonPause.OnPauseButtonClicked += PauseTime;
        ExperienceSystem.OnLevelUp += PauseTime;
        LevelUpChoiceUI.OnUpgrade += ResumeTime;
    }

    void OnDestroy()
    {
        GameStateManager.OnPlaying -= ResumeTime;
        ButtonResume.OnResumeButtonClicked -= ResumeTime;
        ButtonPause.OnPauseButtonClicked -= PauseTime;
        ExperienceSystem.OnLevelUp -= PauseTime;
        LevelUpChoiceUI.OnUpgrade -= ResumeTime;
    }

    static void PauseTime() => Time.timeScale = 0;
    static void PauseTime(int _) => PauseTime();
    static void ResumeTime() => Time.timeScale = 1;
}
