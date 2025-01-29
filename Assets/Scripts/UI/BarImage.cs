using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BarImage : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    Image _image;
    float _maxValue = 1;
    float _currentValue;

    void Awake() => _image = GetComponent<Image>();
    void UpdateBar()
    {
        _image.fillAmount = _currentValue / _maxValue;
        text.text = $"{_currentValue}/{_maxValue}";
    }

    protected void SetMaxValue(float value)
    {
        _maxValue = value;
        UpdateBar();
    }

    protected void SetValue(int value) => SetValue((float)value);
    protected void SetValue(float value)
    {
        _currentValue = value;
        UpdateBar();
    }
}
