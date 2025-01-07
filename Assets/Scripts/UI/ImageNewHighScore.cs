using UnityEngine;
using UnityEngine.UI;

public class ImageNewHighScore : MonoBehaviour
{
    Image _image;
    void Awake() => _image = GetComponent<Image>();
    void OnEnable() => SetImage();
    void SetImage() => _image.enabled = ScoreManager.Score == ScoreManager.HighScore;
}
