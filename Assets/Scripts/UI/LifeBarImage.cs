using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeBarImage : MonoBehaviour
{
    Image _image;
    void Awake() => _image = GetComponent<Image>();
    void Start()
    {
        PlayerHealth.OnPlayerHealthChanged += UpdateLifeBar;
        _image.fillAmount = 1;
    }
    void OnDestroy() => PlayerHealth.OnPlayerHealthChanged -= UpdateLifeBar;
    void UpdateLifeBar(int health) => _image.fillAmount = (float)health / PlayerHealth.MaxHealth;
}
