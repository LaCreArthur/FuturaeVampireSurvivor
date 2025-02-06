using System;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerUpgradables
{
    public static readonly List<Weapon> Weapons = new List<Weapon>();
    public static readonly List<PowerUp> PowerUps = new List<PowerUp>();
    public static event Action<UpgradableSO> OnUpgradableAdded;

    public static void AddUpgradable(Upgradable upgradable)
    {
        if (upgradable is PowerUp powerUp)
            PowerUps.Add(powerUp);
        else if (upgradable is Weapon weapon)
        {
            weapon.ApplyPowerUps();
            Weapons.Add(weapon);
        }

        Debug.Log($"PlayerUpgradable: Added {upgradable.upgradable.name}");
    }

    public static void RemoveUpgradable(Upgradable upgradable)
    {
        if (upgradable is PowerUp powerUp)
            PowerUps.Remove(powerUp);
        else if (upgradable is Weapon weapon)
            Weapons.Remove(weapon);

        Debug.Log($"PlayerUpgradable: Removed {upgradable.upgradable.name}");
    }

    public static void AddOrUpgrade(UpgradableSO upgradableSO)
    {
        Upgradable upgradable = upgradableSO.isPowerUp ?
            PowerUps.Find(p => p.upgradable == upgradableSO) :
            Weapons.Find(w => w.upgradable == upgradableSO);

        if (upgradable == null)
            OnUpgradableAdded?.Invoke(upgradableSO);
        else
            upgradable.Upgrade();
    }

    public static Upgradable GetUpgradableBehavior(UpgradableSO upgradableSO)
    {
        Weapon weapon = Weapons.Find(w => w.upgradable == upgradableSO);
        if (weapon != null)
            return weapon;
        return PowerUps.Find(p => p.upgradable == upgradableSO);
    }
}
