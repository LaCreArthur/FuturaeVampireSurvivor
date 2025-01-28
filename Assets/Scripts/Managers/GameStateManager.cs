using System;
using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static Action OnHome, OnPlaying, OnGameOver, OnPause, OnLeaderboard;
    public static Action<GameState> OnStateChange;
    public static bool IsGamePaused { get; private set; }
    public static GameState CurrentState { get; private set; }

    void Awake() => CurrentState = GameState.Loading;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        SetState(GameState.Home);
    }

    public static void SetState(GameState newState)
    {
        if (newState == CurrentState)
        {
            Debug.LogWarning("Trying to set the same state as the current one.");
            return;
        }

        // If we're pausing the game, set IsGamePaused; otherwise reset it
        IsGamePaused = newState == GameState.Pause;
        CurrentState = newState;
        OnStateChange?.Invoke(CurrentState);
        Debug.Log($"Game State changed to {newState}");

        // Invoke the corresponding event
        switch (newState)
        {
            case GameState.Home:
                OnHome?.Invoke();
                break;
            case GameState.Playing:
                OnPlaying?.Invoke();
                break;
            case GameState.GameOver:
                OnGameOver?.Invoke();
                break;
            case GameState.Pause:
                OnPause?.Invoke();
                break;
            case GameState.Leaderboard:
                OnLeaderboard?.Invoke();
                break;
        }
    }
}

public enum GameState
{
    Loading,
    Home,
    Playing,
    GameOver,
    Pause,
    Leaderboard,
}
