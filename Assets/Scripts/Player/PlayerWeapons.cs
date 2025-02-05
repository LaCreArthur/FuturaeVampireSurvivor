using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerWeapons", fileName = "PlayerWeapons", order = 0)]
public class PlayerWeapons : ScriptableObject
{
    readonly List<WeaponBehavior> _weaponLevels = new List<WeaponBehavior>();

    public static event Action<UpgradableSO> OnNewWeapon;

    public void AddWeapon(WeaponBehavior upgradable)
    {
        _weaponLevels.Add(upgradable);
        Debug.Log($"PlayerWeapon: Added {upgradable.upgradable.name}");
    }

    public void UpgradeWeapon(UpgradableSO upgradable)
    {
        WeaponBehavior behavior = _weaponLevels.Find(w => w.upgradable == upgradable);
        if (behavior == null)
            OnNewWeapon?.Invoke(upgradable);
        else
            behavior.Upgrade();
    }
    public void RemoveWeapon(WeaponBehavior upgradable)
    {
        if (!_weaponLevels.Remove(upgradable))
        {
            Debug.LogError($"PlayerWeapon.RemoveWeapon: {upgradable.upgradable.name} not found in weaponLevels");
        }
    }
    public WeaponBehavior GetWeaponBehavior(UpgradableSO upgradableSO) => _weaponLevels.Find(w => w.upgradable == upgradableSO);
}
