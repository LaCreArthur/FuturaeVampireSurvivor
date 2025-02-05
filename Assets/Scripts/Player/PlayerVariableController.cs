using ScriptableVariables;
using UnityEngine;

/// <summary>
///     Convenient way to access player's components from other scripts.
///     This works because there is only one player in the game.<br></br>
///     For example: <br></br>
///     The enemies need to access the player's transform to follow him.
///     They cannot have a reference to the player's GameObject because they are prefabs instantiated at runtime.
/// </summary>
public class PlayerVariableController : MonoBehaviour
{
    [SerializeField] FloatVar playerHp;
    [SerializeField] FloatVar playerMaxHp;
    [SerializeField] FloatVar playerExp;
    [SerializeField] FloatVar playerMaxExp;
    [SerializeField] TransformVar playerTransform;
    [SerializeField] PlayerUpgradables playerUpgradables;

    void Awake()
    {
        playerTransform.Value = transform;
        PlayerHealthSystem.OnHealthChanged += SetHealth;
        PlayerHealthSystem.OnMaxHealthChanged += SetMaxHealth;
        ExperienceSystem.OnExpChanged += SetExp;
        ExperienceSystem.OnMaxExpChanged += SetMaxExp;
        WeaponSystem.OnWeaponAdded += playerUpgradables.AddUpgradable;
        WeaponSystem.OnWeaponRemoved += playerUpgradables.RemoveUpgradable;
    }

    void OnDestroy()
    {
        PlayerHealthSystem.OnHealthChanged -= SetHealth;
        PlayerHealthSystem.OnMaxHealthChanged -= SetMaxHealth;
        ExperienceSystem.OnExpChanged -= SetExp;
        ExperienceSystem.OnMaxExpChanged -= SetMaxExp;
        WeaponSystem.OnWeaponAdded -= playerUpgradables.AddUpgradable;
        WeaponSystem.OnWeaponRemoved -= playerUpgradables.RemoveUpgradable;
    }

    void SetHealth(int health) => playerHp.Value = health;
    void SetMaxHealth(int maxHealth) => playerMaxHp.Value = maxHealth;
    void SetExp(float exp) => playerExp.Value = exp;
    void SetMaxExp(float maxExp) => playerMaxExp.Value = maxExp;
}
