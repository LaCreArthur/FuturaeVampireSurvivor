using ScriptableVariables;
using UnityEngine;

/// <summary>
///     Convenient way to access player's components from other scripts.
///     This works because there is only one player in the game.<br></br>
///     For example: <br></br>
///     The enemies need to access the player's transform to follow him.
///     They cannot have a reference to the player's GameObject because they are prefabs instantiated at runtime.
/// </summary>
public class PlayerVariableInitializer : MonoBehaviour
{
    [SerializeField] FloatVar playerHp;
    [SerializeField] FloatVar playerMaxHp;
    [SerializeField] FloatVar playerExp;
    [SerializeField] FloatVar playerMaxExp;
    [SerializeField] TransformVar playerTransform;

    HealthSystem _healthSystem;
    WeaponSystem _playerWeaponSystem;
    ExperienceSystem _experienceSystem;

    void Awake()
    {
        playerTransform.Value = transform;
        _healthSystem = GetComponent<HealthSystem>();
        _playerWeaponSystem = GetComponent<WeaponSystem>();
        _experienceSystem = GetComponent<ExperienceSystem>();
        _healthSystem.OnHealthChanged += SetHealth;
        _healthSystem.OnMaxHealthChanged += SetMaxHealth;
        _experienceSystem.OnExpChanged += SetExp;
        _experienceSystem.OnMaxExpChanged += SetMaxExp;
    }

    void OnDestroy()
    {
        _healthSystem.OnHealthChanged -= SetHealth;
        _healthSystem.OnMaxHealthChanged -= SetMaxHealth;
        _experienceSystem.OnExpChanged -= SetExp;
        _experienceSystem.OnMaxExpChanged -= SetMaxExp;
    }

    void SetHealth(int health) => playerHp.Value = health;
    void SetMaxHealth(int maxHealth) => playerMaxHp.Value = maxHealth;
    void SetExp(float exp) => playerExp.Value = exp;
    void SetMaxExp(float maxExp) => playerMaxExp.Value = maxExp;
}
