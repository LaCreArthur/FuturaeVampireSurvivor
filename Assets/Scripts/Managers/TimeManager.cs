using UnityEngine;

public class TimeManager : MonoBehaviour
{
    void Start()
    {
        GameStateManager.OnPlaying += ResumeTime;
        ButtonResume.OnResumeButtonClicked += ResumeTime;
        ButtonPause.OnPauseButtonClicked += PauseTime;
        PlayerExperienceSystem.OnLevelUp += PauseTime;
    }

    void OnDestroy()
    {
        GameStateManager.OnPlaying -= ResumeTime;
        ButtonResume.OnResumeButtonClicked -= ResumeTime;
        ButtonPause.OnPauseButtonClicked -= PauseTime;
        PlayerExperienceSystem.OnLevelUp -= PauseTime;
    }

    static void PauseTime(int _) => PauseTime();
    static void PauseTime() => Time.timeScale = 0;
    static void ResumeTime() => Time.timeScale = 1;
}
