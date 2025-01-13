using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameState CurrentState { get; private set; }
    void Start() => CurrentState = GameState.Menu;

    public static void SetState(GameState newState)
    {
        if (newState != CurrentState)
            CurrentState = newState;
        else Debug.LogWarning("Trying to set the same state as the current one.");
    }
}

public enum GameState
{
    Menu,
    InGame,
    GameOver,
    Pause,
}
