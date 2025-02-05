using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerWeapons", fileName = "PlayerWeapons", order = 0)]
public class PlayerWeapons : ScriptableObject
{
    readonly List<UpgradableBehavior> _weaponLevels = new List<UpgradableBehavior>();

    public static event Action<UpgradableSO> OnNewWeapon;

    public void AddWeapon(UpgradableBehavior upgradable)
    {
        _weaponLevels.Add(upgradable);
        Debug.Log($"PlayerWeapon: Added {upgradable.upgradable.name}");
    }

    public void UpgradeWeapon(UpgradableSO upgradable)
    {
        UpgradableBehavior behavior = _weaponLevels.Find(w => w.upgradable == upgradable);
        if (behavior == null)
            OnNewWeapon?.Invoke(upgradable);
        else
            behavior.Upgrade();
    }
    public void RemoveWeapon(UpgradableBehavior upgradable)
    {
        if (!_weaponLevels.Remove(upgradable))
        {
            Debug.LogError($"PlayerWeapon.RemoveWeapon: {upgradable.upgradable.name} not found in weaponLevels");
        }
    }
    public UpgradableBehavior GetWeaponBehavior(UpgradableSO upgradableSO) => _weaponLevels.Find(w => w.upgradable == upgradableSO);
}
