using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextEndScore : MonoBehaviour
{
    TextMeshProUGUI _text;
    void Awake() => _text = GetComponent<TextMeshProUGUI>();
    void OnEnable() => UpdateScoreText();
    void UpdateScoreText() => _text.text =
        ScoreManager.Score == ScoreManager.HighScore ? $"New high score: {ScoreManager.HighScore}!" :
            $"score: {ScoreManager.Score} - High score: {ScoreManager.HighScore}";
}
