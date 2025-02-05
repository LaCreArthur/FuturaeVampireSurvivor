using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/Weapon", fileName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public Sprite sprite;
    public GameObject weaponPrefab;
    public string baseDescription;
    public WeaponLevelData[] levelData;

    public AudioClip fireSound;
    public GameObject projectilePrefab;
    public string GetUpgradeDescription(int weaponLevel)
    {
        if (weaponLevel == -1)
        {
            return baseDescription;
        }
        if (weaponLevel + 1 >= levelData.Length)
        {
            return "Max level reached, you should not see this!";
        }

        // Get the data for the previous level and the current one
        WeaponLevelData oldData = levelData[weaponLevel];
        WeaponLevelData newData = levelData[weaponLevel + 1];

        // Keep track of which stats changed
        var changes = new List<string>();

        if (newData.damage > oldData.damage)
        {
            int diff = newData.damage - oldData.damage;
            changes.Add($"+{diff} Damage");
        }

        if (newData.cooldown < oldData.cooldown)
        {
            float diff = oldData.cooldown - newData.cooldown;
            changes.Add($"-{diff:F1}s Cooldown");
        }

        if (newData.area > oldData.area)
        {
            float diff = newData.area - oldData.area;
            changes.Add($"+{diff:F1} Area");
        }

        if (newData.amount > oldData.amount)
        {
            int diff = newData.amount - oldData.amount;
            changes.Add($"+{diff} Projectiles");
        }

        if (newData.duration > oldData.duration)
        {
            float diff = newData.duration - oldData.duration;
            changes.Add($"+{diff:F1}s Duration");
        }

        if (newData.pierce > oldData.pierce)
        {
            int diff = newData.pierce - oldData.pierce;
            changes.Add($"+{diff} Pierce");
        }

        if (newData.projectileInterval < oldData.projectileInterval)
        {
            float diff = oldData.projectileInterval - newData.projectileInterval;
            changes.Add($"-{diff:F1}s Between Projectiles");
        }

        if (newData.knockback > oldData.knockback)
        {
            float diff = newData.knockback - oldData.knockback;
            changes.Add($"+{diff:F1} Knockback");
        }


        // Join all improvements into one line
        return changes.Count > 0
            ? string.Join(", ", changes)
            : "No stat change";

    }
}
