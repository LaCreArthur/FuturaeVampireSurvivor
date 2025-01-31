using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeBarImage : BarImage
{
    HealthSystem _playerHealth;

    void Start()
    {
        _playerHealth = PlayerStaticReferences.PlayerHealthSystem;
        _playerHealth.OnHealthChanged += SetValue;
        SetMaxValue(_playerHealth.MaxHealth);
        SetValue(_playerHealth.CurrentHealth);
    }

    void OnDestroy() => _playerHealth.OnHealthChanged -= SetValue;
}
