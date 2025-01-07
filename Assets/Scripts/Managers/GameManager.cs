using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnGameStart, OnGameEnd, OnGameRestart, OnGamePause, OnGameResume, OnLeaderboard;

    void Start()
    {
        OnGameStart += StartGame;
        OnGameEnd += PauseTime;
        OnGamePause += PauseTime;
        OnGameResume += ResumeTime;
        Time.timeScale = 0;
    }

    void OnDestroy()
    {
        OnGameStart -= StartGame;
        OnGameResume -= ResumeTime;
        OnGameEnd -= PauseTime;
        OnGamePause -= PauseTime;
    }

    static void StartGame()
    {
        ScoreManager.Score = 0;
        ResumeTime();
    }

    static void PauseTime() => Time.timeScale = 0;
    static void ResumeTime() => Time.timeScale = 1;
}
