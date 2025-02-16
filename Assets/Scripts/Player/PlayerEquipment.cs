using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : SingletonMono<PlayerEquipment>
{
    public const int MAX_SLOTS = 4;

    public static readonly List<Weapon> Weapons = new List<Weapon>();
    public static readonly List<PowerUp> PowerUps = new List<PowerUp>();

    static Transform s_weaponParent;

    [SerializeField] Transform weaponParent;
    public static event Action<UpgradableSO> OnUpgradableAdded;
    public static event Action<UpgradableSO> OnUpgradableUpgraded;

    public static bool CanAddWeapon => Weapons.Count < MAX_SLOTS;
    public static bool CanAddPowerUp => PowerUps.Count < MAX_SLOTS;

    protected override void OnAwake()
    {
        s_weaponParent = weaponParent;
        CharacterSelector.CharacterChanged += OnCharacterChanged;
    }

    void OnDestroy() => CharacterSelector.CharacterChanged -= OnCharacterChanged;

    void OnCharacterChanged(CharacterSO characterSO)
    {
        RemoveAllEquipment();
        Add(characterSO.weaponSO);
    }

    static void RemoveAllEquipment()
    {
        for (int i = Weapons.Count - 1; i >= 0; i--)
        {
            Weapon weapon = Weapons[i];
            Remove(weapon);
        }
        for (int i = PowerUps.Count - 1; i >= 0; i--)
        {
            PowerUp powerUp = PowerUps[i];
            Remove(powerUp);
        }
    }

    public static void Remove(Upgradable upgradable)
    {
        if (upgradable is PowerUp powerUp)
        {
            PowerUps.Remove(powerUp);
            Debug.Log($"PlayerEquipment: Removed power up {powerUp.upgradable.name}");
        }
        else if (upgradable is Weapon weapon)
        {
            Weapons.Remove(weapon);
            Debug.Log($"PlayerEquipment: Removed weapon {weapon.upgradable.name}");
        }

        Destroy(upgradable.gameObject);
    }

    public static Upgradable GetInstance(UpgradableSO upgradableSO)
    {
        Weapon weapon = Weapons.Find(w => w.upgradable == upgradableSO);
        if (weapon != null)
            return weapon;
        return PowerUps.Find(p => p.upgradable == upgradableSO);
    }

    public static void Add(UpgradableSO upgradableSO)
    {
        GameObject go = Instantiate(upgradableSO.prefab, s_weaponParent.position, Quaternion.identity, s_weaponParent);
        var upgradable = go.GetComponent<Upgradable>();

        if (upgradable is PowerUp powerUp)
            PowerUps.Add(powerUp);
        else if (upgradable is Weapon weapon)
        {
            Weapons.Add(weapon);
            // we need to initialize the modifiedStats to the base stats
            weapon.modifiedStats = weapon.Stats;
        }

        Debug.Log($"PlayerEquipment: Added {upgradableSO.name}");
        OnUpgradableAdded?.Invoke(upgradableSO);
    }
    public static void Upgrade(UpgradableSO upgradableSO)
    {
        Upgradable upgradable = upgradableSO is PowerUpSO ?
            PowerUps.Find(p => p.upgradable == upgradableSO) :
            Weapons.Find(w => w.upgradable == upgradableSO);
        upgradable.Upgrade();
        OnUpgradableUpgraded?.Invoke(upgradableSO);
    }
}
