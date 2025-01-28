using UnityEngine;

public class TimeManager : MonoBehaviour
{
    void Start()
    {
        GameStateManager.OnPause += PauseTime;
        GameStateManager.OnPlaying += ResumeTime;
    }

    void OnDestroy()
    {
        GameStateManager.OnPause -= PauseTime;
        GameStateManager.OnPlaying -= ResumeTime;
    }

    static void PauseTime() => Time.timeScale = 0;
    static void ResumeTime() => Time.timeScale = 1;
}
