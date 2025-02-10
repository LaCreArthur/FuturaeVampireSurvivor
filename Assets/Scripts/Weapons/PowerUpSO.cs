using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/PowerUp", fileName = "PowerUpSO")]
public class PowerUpSO : UpgradableSO<PowerUpStats>
{
    public override string GetUpgradeDescription(int level)
    {
        if (level == -1)
        {
            return baseDescription;
        }
        if (level + 1 >= levelData.Length)
        {
            return "Max level reached, you should not see this!";
        }

        // Get the stats for the previous level and the current one
        PowerUpStats oldStats = levelData[level];
        PowerUpStats newStats = levelData[level + 1];

        // Keep track of which stats changed
        var changes = new List<string>();

        if (Mathf.Abs(newStats.weaponStats.damage - oldStats.weaponStats.damage) != 0)
        {
            int diff = newStats.weaponStats.damage - oldStats.weaponStats.damage;
            changes.Add($"+{diff}% Damage");
        }

        if (Mathf.Abs(newStats.weaponStats.cooldown - oldStats.weaponStats.cooldown) != 0)
        {
            float diff = oldStats.weaponStats.cooldown - newStats.weaponStats.cooldown;
            changes.Add($"-{diff:F1}% Cooldown");
        }

        if (Mathf.Abs(newStats.weaponStats.area - oldStats.weaponStats.area) != 0)
        {
            float diff = newStats.weaponStats.area - oldStats.weaponStats.area;
            changes.Add($"+{diff:F1}% Area");
        }

        if (Mathf.Abs(newStats.weaponStats.amount - oldStats.weaponStats.amount) != 0)
        {
            int diff = newStats.weaponStats.amount - oldStats.weaponStats.amount;
            changes.Add($"+{diff} Projectiles");
        }

        if (Mathf.Abs(newStats.weaponStats.duration - oldStats.weaponStats.duration) != 0)
        {
            float diff = newStats.weaponStats.duration - oldStats.weaponStats.duration;
            changes.Add($"+{diff:F1}% Duration");
        }

        if (Mathf.Abs(newStats.weaponStats.pierce - oldStats.weaponStats.pierce) != 0)
        {
            int diff = newStats.weaponStats.pierce - oldStats.weaponStats.pierce;
            changes.Add($"+{diff} Pierce");
        }

        if (Mathf.Abs(newStats.weaponStats.projectileInterval - oldStats.weaponStats.projectileInterval) != 0)
        {
            float diff = oldStats.weaponStats.projectileInterval - newStats.weaponStats.projectileInterval;
            changes.Add($"-{diff:F1}% Between Projectiles");
        }

        if (Mathf.Abs(newStats.weaponStats.knockback - oldStats.weaponStats.knockback) != 0)
        {
            float diff = newStats.weaponStats.knockback - oldStats.weaponStats.knockback;
            changes.Add($"+{diff:F1}% Knockback");
        }

        if (Mathf.Abs(newStats.playerStats.maxHealth - oldStats.playerStats.maxHealth) != 0)
        {
            int diff = newStats.playerStats.maxHealth - oldStats.playerStats.maxHealth;
            changes.Add($"+{diff:F1}% Max HP");
        }

        if (Mathf.Abs(newStats.playerStats.recovery - oldStats.playerStats.recovery) != 0)
        {
            float diff = newStats.playerStats.recovery - oldStats.playerStats.recovery;
            changes.Add($"+{diff} Recovery");
        }

        if (Mathf.Abs(newStats.playerStats.moveSpeed - oldStats.playerStats.moveSpeed) != 0)
        {
            float diff = newStats.playerStats.moveSpeed - oldStats.playerStats.moveSpeed;
            changes.Add($"+{diff:F1}% Move Speed");
        }

        if (Mathf.Abs(newStats.playerStats.attractionRange - oldStats.playerStats.attractionRange) != 0)
        {
            float diff = newStats.playerStats.attractionRange - oldStats.playerStats.attractionRange;
            changes.Add($"+{diff:F1}% Attraction Range");
        }

        if (Mathf.Abs(newStats.playerStats.armor - oldStats.playerStats.armor) != 0)
        {
            int diff = newStats.playerStats.armor - oldStats.playerStats.armor;
            changes.Add($"+{diff} Armor");
        }

        if (Mathf.Abs(newStats.playerStats.growth - oldStats.playerStats.growth) != 0)
        {
            int diff = newStats.playerStats.growth - oldStats.playerStats.growth;
            changes.Add($"+{diff:F1}% Exp Gain");
        }

        // Join all improvements into one line
        return changes.Count > 0
            ? string.Join(", ", changes)
            : "No stat change";
    }
}

[Serializable]
public struct PowerUpStats
{
    public WeaponStats weaponStats;
    public PlayerStats playerStats;
}
