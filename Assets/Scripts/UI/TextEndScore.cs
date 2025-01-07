using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextEndScore : MonoBehaviour
{
    TextMeshProUGUI _text;
    void Awake() => _text = GetComponent<TextMeshProUGUI>();
    void OnEnable() => UpdateScoreText();
    void UpdateScoreText() => _text.text = $"score: {ScoreManager.Score}\nhigh score: {ScoreManager.HighScore}";
}
