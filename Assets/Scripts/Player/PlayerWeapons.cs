using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerWeapons", fileName = "PlayerWeapons", order = 0)]
public class PlayerWeapons : ScriptableObject
{
    public readonly Dictionary<WeaponSO, int> weaponLevels = new Dictionary<WeaponSO, int>();
    public void AddWeapon(WeaponBehavior weapon) => weaponLevels.Add(weapon.weapon, 1);
    public void UpgradeWeapon(WeaponSO weapon)
    {
        weaponLevels[weapon]++;
        Debug.Log($"Upgraded {weapon.name} to level {weaponLevels[weapon]}");
    }
    public void RemoveWeapon(WeaponBehavior weapon) => weaponLevels.Remove(weapon.weapon);
    public int GetWeaponLevel(WeaponSO weaponSO) => weaponLevels.GetValueOrDefault(weaponSO, 0);
}
