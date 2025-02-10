using System;
using UnityEngine;

public class ModifierSystem : MonoBehaviour
{
    static WeaponStats s_weaponModifier;
    static PlayerStats s_playerModifier;
    public static event Action OnModifiersUpdated;

    void Start()
    {
        PlayerEquipment.OnUpgradableAdded += UpdateModifiers;
        PlayerEquipment.OnUpgradableUpgraded += UpdateModifiers;
    }

    void OnDestroy()
    {
        PlayerEquipment.OnUpgradableAdded -= UpdateModifiers;
        PlayerEquipment.OnUpgradableUpgraded -= UpdateModifiers;
    }


    static void UpdateModifiers(UpgradableSO _)
    {
        // reset the modifiers and apply the power ups
        s_weaponModifier = new WeaponStats();
        s_playerModifier = new PlayerStats();
        foreach (PowerUp powerUp in PlayerEquipment.PowerUps)
        {
            s_weaponModifier = powerUp.ApplyWeaponPowerUp(s_weaponModifier);
            s_playerModifier = powerUp.ApplyPlayerPowerUp(s_playerModifier);
        }
        foreach (Weapon weapon in PlayerEquipment.Weapons)
        {
            weapon.poweredUpStats = ApplyWeaponModifier(weapon.Stats);
        }
        Debug.Log("ModifierSystem: Modifiers updated");
        OnModifiersUpdated?.Invoke();
    }

    public static WeaponStats ApplyWeaponModifier(WeaponStats stats)
    {
        stats.damage += Mathf.RoundToInt(stats.damage * s_weaponModifier.damage / 100f);
        stats.cooldown += stats.cooldown * s_weaponModifier.cooldown / 100f;
        stats.area += stats.area * s_weaponModifier.area / 100f;
        stats.speed += stats.speed * s_weaponModifier.speed / 100f;
        stats.duration += stats.duration * s_weaponModifier.duration / 100f;
        stats.projectileInterval += stats.projectileInterval * s_weaponModifier.projectileInterval / 100f;
        stats.knockback += stats.knockback * s_weaponModifier.knockback / 100f;
        stats.amount += s_weaponModifier.amount;
        stats.pierce += s_weaponModifier.pierce;
        return stats;
    }

    public static PlayerStats ApplyPlayerModifier(PlayerStats stats)
    {
        stats.maxHealth += Mathf.RoundToInt(stats.maxHealth * s_playerModifier.maxHealth / 100f);
        stats.moveSpeed += stats.moveSpeed * s_playerModifier.moveSpeed / 100f;
        stats.attractionRange += stats.attractionRange * s_playerModifier.attractionRange / 100f;
        stats.growth += Mathf.RoundToInt(stats.growth * s_playerModifier.growth / 100f);
        stats.recovery += s_playerModifier.recovery;
        stats.armor += s_playerModifier.armor;
        return stats;
    }
}
