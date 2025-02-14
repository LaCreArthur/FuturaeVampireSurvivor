using System;
using UnityEngine;

public class ModifierSystem : MonoBehaviour
{

    Modifiers _modifiers;
    Modifiers _characterBaseStats;
    public static event Action WeaponModifiersUpdated;
    public static event Action<Modifiers> CharacterModifiersUpdated;

    void Awake()
    {
        PlayerEquipment.OnUpgradableAdded += UpdateModifiers;
        PlayerEquipment.OnUpgradableUpgraded += UpdateModifiers;
        CharacterSelector.CharacterChanged += OnCharacterChanged;
    }

    void OnDestroy()
    {
        PlayerEquipment.OnUpgradableAdded -= UpdateModifiers;
        PlayerEquipment.OnUpgradableUpgraded -= UpdateModifiers;
        CharacterSelector.CharacterChanged -= OnCharacterChanged;
    }
    void OnCharacterChanged(CharacterSO characterSO)
    {
        _characterBaseStats = characterSO.baseStats;
        UpdateModifiers(null);
    }

    void UpdateModifiers(UpgradableSO _)
    {
        // Add all modifiers from power ups
        _modifiers = new Modifiers();
        foreach (PowerUp powerUp in PlayerEquipment.PowerUps)
        {
            _modifiers = powerUp.AddModifier(_modifiers);
        }
        // Apply modifiers to all weapons
        foreach (Weapon weapon in PlayerEquipment.Weapons)
        {
            weapon.modifiedStats = ApplyWeaponModifiers(weapon.Stats);
        }
        WeaponModifiersUpdated?.Invoke();

        Modifiers characterModifiedStats = ApplyCharacterModifiers(_characterBaseStats);
        CharacterModifiersUpdated?.Invoke(characterModifiedStats);

        Debug.Log("ModifierSystem: Modifiers updated");
    }

    WeaponStats ApplyWeaponModifiers(WeaponStats stats)
    {
        stats.damage += Mathf.RoundToInt(stats.damage * _modifiers.might / 100f);
        stats.area += stats.area * _modifiers.area / 100f;
        stats.speed += stats.speed * _modifiers.speed / 100f;
        stats.amount += _modifiers.amount;
        stats.cooldown += stats.cooldown * _modifiers.cooldown / 100f;
        return stats;
    }

    Modifiers ApplyCharacterModifiers(Modifiers stats)
    {
        stats.maxHealth += Mathf.RoundToInt(stats.maxHealth * _modifiers.maxHealth / 100f);
        stats.recovery += _modifiers.recovery;
        stats.armor += _modifiers.armor;
        stats.moveSpeed += stats.moveSpeed * _modifiers.moveSpeed / 100f;
        stats.growth += Mathf.RoundToInt(stats.growth * _modifiers.growth / 100f);
        stats.attractionRange += stats.attractionRange * _modifiers.attractionRange / 100f;
        return stats;
    }
}
