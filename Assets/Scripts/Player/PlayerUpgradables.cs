using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerWeapons", fileName = "PlayerWeapons", order = 0)]
public class PlayerUpgradables : ScriptableObject
{
    readonly List<WeaponBehavior> _weaponLevels = new List<WeaponBehavior>();
    readonly List<PowerUpBehavior> _powerUpLevels = new List<PowerUpBehavior>();

    public static event Action<UpgradableSO> OnNewWeapon;
    public static event Action<UpgradableSO> OnNewPowerUp;

    public void AddUpgradable(UpgradableBehavior upgradable)
    {
        if (upgradable is PowerUpBehavior powerUp)
            _powerUpLevels.Add(powerUp);
        else if (upgradable is WeaponBehavior weapon)
            _weaponLevels.Add(weapon);

        Debug.Log($"PlayerUpgradable: Added {upgradable.upgradable.name}");
    }

    public void Upgrade(UpgradableSO upgradable)
    {
        if (upgradable.isPowerUp)
        {
            PowerUpBehavior powerUpBehavior = _powerUpLevels.Find(p => p.upgradable == upgradable);
            if (powerUpBehavior == null)
                OnNewPowerUp?.Invoke(upgradable);
            else
                powerUpBehavior.Upgrade();
        }
        else
        {
            WeaponBehavior weaponBehavior = _weaponLevels.Find(w => w.upgradable == upgradable);
            if (weaponBehavior == null)
                OnNewWeapon?.Invoke(upgradable);
            else
                weaponBehavior.Upgrade();
        }
    }

    public void RemoveUpgradable(UpgradableBehavior upgradable)
    {
        if (upgradable is PowerUpBehavior powerUp && !_powerUpLevels.Remove(powerUp))
            Debug.LogError($"PlayerWeapon.RemovePowerUp: {upgradable.upgradable.name} not found in powerUpLevels");
        else if (upgradable is WeaponBehavior weapon && !_weaponLevels.Remove(weapon))
            Debug.LogError($"PlayerWeapon.RemoveWeapon: {upgradable.upgradable.name} not found in weaponLevels");
    }

    public WeaponBehavior GetWeaponBehavior(UpgradableSO upgradableSO) => _weaponLevels.Find(w => w.upgradable == upgradableSO);
}
