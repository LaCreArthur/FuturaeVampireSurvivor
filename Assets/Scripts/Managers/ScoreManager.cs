using System;
using UnityEngine;

public static class ScoreManager
{
    static int s_score;
    static int s_highScore;
    public static int Score
    {
        get => s_score;
        set {
            s_score = value;
            OnScoreUpdated?.Invoke(value);
            if (value > HighScore)
                HighScore = value;
        }
    }

    public static int HighScore
    {
        // if s_highScore is 0, get the high score from PlayerPrefs
        get => s_highScore == 0 ? PlayerPrefs.GetInt("highScore", 0) : s_highScore;
        private set {
            s_highScore = value;
            PlayerPrefs.SetInt("highScore", value);
        }
    }

    public static event Action<int> OnScoreUpdated;
    public static void IncreaseScore() => Score++;
}
