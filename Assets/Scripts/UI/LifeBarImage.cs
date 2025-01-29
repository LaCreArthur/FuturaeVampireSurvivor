using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeBarImage : BarImage
{
    void Start()
    {
        PlayerHealth.OnPlayerHealthChanged += SetValue;
        SetMaxValue(PlayerHealth.MaxHealth);
        SetValue(PlayerHealth.CurrentHealth);
    }

    void OnDestroy() => PlayerHealth.OnPlayerHealthChanged -= SetValue;
}
