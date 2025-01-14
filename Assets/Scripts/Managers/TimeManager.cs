using UnityEngine;

public class TimeManager : MonoBehaviour
{
    void Start()
    {
        GameStateManager.OnHome += PauseTime;
        GameStateManager.OnPause += PauseTime;
        GameStateManager.OnInGame += ResumeTime;
        Time.timeScale = 0;
    }

    void OnDestroy()
    {
        GameStateManager.OnHome -= PauseTime;
        GameStateManager.OnPause -= PauseTime;
        GameStateManager.OnInGame -= ResumeTime;
    }

    static void PauseTime() => Time.timeScale = 0;
    static void ResumeTime() => Time.timeScale = 1;
}
