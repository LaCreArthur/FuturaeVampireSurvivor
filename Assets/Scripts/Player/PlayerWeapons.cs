using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerWeapons", fileName = "PlayerWeapons", order = 0)]
public class PlayerWeapons : ScriptableObject
{
    public List<WeaponBehavior> weaponLevels = new List<WeaponBehavior>();

    public static event Action<WeaponSO> OnNewWeapon;

    public void AddWeapon(WeaponBehavior weapon)
    {
        weaponLevels.Add(weapon);
        Debug.Log($"PlayerWeapon: Added {weapon.weapon.name}");
    }

    public void UpgradeWeapon(WeaponSO weapon)
    {
        WeaponBehavior behavior = weaponLevels.Find(w => w.weapon == weapon);
        if (behavior == null)
            OnNewWeapon?.Invoke(weapon);
        else
            behavior.UpgradeWeapon();
    }
    public void RemoveWeapon(WeaponBehavior weapon)
    {
        if (!weaponLevels.Remove(weapon))
        {
            Debug.LogError($"PlayerWeapon.RemoveWeapon: {weapon.weapon.name} not found in weaponLevels");
        }
    }
    public WeaponBehavior GetWeaponBehavior(WeaponSO weaponSO) => weaponLevels.Find(w => w.weapon == weaponSO);
}
