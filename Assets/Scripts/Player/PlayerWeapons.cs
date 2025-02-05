using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerWeapons", fileName = "PlayerWeapons", order = 0)]
public class PlayerWeapons : ScriptableObject
{
    public List<WeaponBehavior> weaponLevels = new List<WeaponBehavior>();

    public static event Action<UpgradableSO> OnNewWeapon;

    public void AddWeapon(WeaponBehavior weapon)
    {
        weaponLevels.Add(weapon);
        Debug.Log($"PlayerWeapon: Added {weapon.upgradable.name}");
    }

    public void UpgradeWeapon(UpgradableSO upgradable)
    {
        WeaponBehavior behavior = weaponLevels.Find(w => w.upgradable == upgradable);
        if (behavior == null)
            OnNewWeapon?.Invoke(upgradable);
        else
            behavior.UpgradeWeapon();
    }
    public void RemoveWeapon(WeaponBehavior weapon)
    {
        if (!weaponLevels.Remove(weapon))
        {
            Debug.LogError($"PlayerWeapon.RemoveWeapon: {weapon.upgradable.name} not found in weaponLevels");
        }
    }
    public WeaponBehavior GetWeaponBehavior(UpgradableSO upgradableSO) => weaponLevels.Find(w => w.upgradable == upgradableSO);
}
