using System;
using UnityEngine;

public class ModifierSystem : MonoBehaviour
{
    static Modifiers s_modifiers;
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
        s_modifiers = new Modifiers();
        foreach (PowerUp powerUp in PlayerEquipment.PowerUps)
        {
            s_modifiers = powerUp.AddModifier(s_modifiers);
        }
        foreach (Weapon weapon in PlayerEquipment.Weapons)
        {
            weapon.modifiedStats = ApplyModifiers(weapon.Stats);
        }
        Debug.Log("ModifierSystem: Modifiers updated");
        OnModifiersUpdated?.Invoke();
    }

    public static WeaponStats ApplyModifiers(WeaponStats stats)
    {
        stats.damage += Mathf.RoundToInt(stats.damage * s_modifiers.might / 100f);
        stats.area += stats.area * s_modifiers.area / 100f;
        stats.speed += stats.speed * s_modifiers.speed / 100f;
        stats.amount += s_modifiers.amount;
        stats.cooldown += stats.cooldown * s_modifiers.cooldown / 100f;
        return stats;
    }
}
