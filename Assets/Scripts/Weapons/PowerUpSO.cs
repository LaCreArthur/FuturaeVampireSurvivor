using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/Upgradable", fileName = "UpgradableSO")]
public class PowerUpSO : UpgradableSOBase
{
    public bool isPowerUp;
    public WeaponStats[] levelData;

    public string GetUpgradeDescription(int weaponLevel)
    {
        if (weaponLevel == -1 || isPowerUp)
        {
            return baseDescription;
        }
        if (weaponLevel + 1 >= levelData.Length)
        {
            return "Max level reached, you should not see this!";
        }

        // Get the stats for the previous level and the current one
        WeaponStats oldStats = levelData[weaponLevel];
        WeaponStats newStats = levelData[weaponLevel + 1];

        // Keep track of which stats changed
        var changes = new List<string>();

        if (Mathf.Abs(newStats.damage - oldStats.damage) != 0)
        {
            int diff = newStats.damage - oldStats.damage;
            changes.Add($"+{diff}{(isPowerUp ? "%" : "")} Damage");
        }

        if (Mathf.Abs(newStats.cooldown - oldStats.cooldown) != 0)
        {
            float diff = oldStats.cooldown - newStats.cooldown;
            changes.Add($"-{diff:F1}{(isPowerUp ? "%" : "s")} Cooldown");
        }

        if (Mathf.Abs(newStats.area - oldStats.area) != 0)
        {
            float diff = newStats.area - oldStats.area;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "")} Area");
        }

        if (Mathf.Abs(newStats.amount - oldStats.amount) != 0)
        {
            int diff = newStats.amount - oldStats.amount;
            changes.Add($"+{diff} Projectiles");
        }

        if (Mathf.Abs(newStats.duration - oldStats.duration) != 0)
        {
            float diff = newStats.duration - oldStats.duration;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "s")} Duration");
        }

        if (Mathf.Abs(newStats.pierce - oldStats.pierce) != 0)
        {
            int diff = newStats.pierce - oldStats.pierce;
            changes.Add($"+{diff} Pierce");
        }

        if (Mathf.Abs(newStats.projectileInterval - oldStats.projectileInterval) != 0)
        {
            float diff = oldStats.projectileInterval - newStats.projectileInterval;
            changes.Add($"-{diff:F1}{(isPowerUp ? "%" : "s")} Between Projectiles");
        }

        if (Mathf.Abs(newStats.knockback - oldStats.knockback) != 0)
        {
            float diff = newStats.knockback - oldStats.knockback;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "")} Knockback");
        }

        // Join all improvements into one line
        return changes.Count > 0
            ? string.Join(", ", changes)
            : "No stat change";
    }
}
