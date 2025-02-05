using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/Upgradable", fileName = "UpgradableSO")]
public class UpgradableSO : ScriptableObject
{
    public Sprite sprite;
    public GameObject prefab;
    public string baseDescription;
    public UpgradableLevelData[] levelData;
    public bool isPowerUp;

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
        UpgradableLevelData oldData = levelData[weaponLevel];
        UpgradableLevelData newData = levelData[weaponLevel + 1];

        // Keep track of which stats changed
        var changes = new List<string>();

        if (newData.damage > oldData.damage)
        {
            int diff = newData.damage - oldData.damage;
            changes.Add($"+{diff}{(isPowerUp ? "%" : "")} Damage");
        }

        if (newData.cooldown < oldData.cooldown)
        {
            float diff = oldData.cooldown - newData.cooldown;
            changes.Add($"-{diff:F1}{(isPowerUp ? "%" : "s")} Cooldown");
        }

        if (newData.area > oldData.area)
        {
            float diff = newData.area - oldData.area;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "")} Area");
        }

        if (newData.amount > oldData.amount)
        {
            int diff = newData.amount - oldData.amount;
            changes.Add($"+{diff} Projectiles");
        }

        if (newData.duration > oldData.duration)
        {
            float diff = newData.duration - oldData.duration;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "s")} Duration");
        }

        if (newData.pierce > oldData.pierce)
        {
            int diff = newData.pierce - oldData.pierce;
            changes.Add($"+{diff} Pierce");
        }

        if (newData.projectileInterval < oldData.projectileInterval)
        {
            float diff = oldData.projectileInterval - newData.projectileInterval;
            changes.Add($"-{diff:F1}{(isPowerUp ? "%" : "s")} Between Projectiles");
        }

        if (newData.knockback > oldData.knockback)
        {
            float diff = newData.knockback - oldData.knockback;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "")} Knockback");
        }

        // Join all improvements into one line
        return changes.Count > 0
            ? string.Join(", ", changes)
            : "No stat change";
    }
}
