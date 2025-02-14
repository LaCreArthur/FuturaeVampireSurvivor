using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/PowerUp", fileName = "PowerUpSO")]
public class PowerUpSO : UpgradableSO<Modifiers>
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
        Modifiers oldStats = levelData[level];
        Modifiers newStats = levelData[level + 1];

        // Keep track of which stats changed
        var changes = new List<string>();

        if (Mathf.Abs(newStats.maxHealth - oldStats.maxHealth) != 0)
        {
            int diff = newStats.maxHealth - oldStats.maxHealth;
            changes.Add($"+{diff:F1}% Max HP");
        }
        if (Mathf.Abs(newStats.recovery - oldStats.recovery) != 0)
        {
            float diff = newStats.recovery - oldStats.recovery;
            changes.Add($"+{diff} Recovery");
        }
        if (Mathf.Abs(newStats.armor - oldStats.armor) != 0)
        {
            int diff = newStats.armor - oldStats.armor;
            changes.Add($"+{diff} Armor");
        }
        if (Mathf.Abs(newStats.moveSpeed - oldStats.moveSpeed) != 0)
        {
            float diff = newStats.moveSpeed - oldStats.moveSpeed;
            changes.Add($"+{diff:F1}% Move Speed");
        }
        if (Mathf.Abs(newStats.might - oldStats.might) != 0)
        {
            float diff = newStats.might - oldStats.might;
            changes.Add($"+{diff}% Damage");
        }
        if (Mathf.Abs(newStats.area - oldStats.area) != 0)
        {
            float diff = newStats.area - oldStats.area;
            changes.Add($"+{diff:F1}% Area");
        }
        if (Mathf.Abs(newStats.speed - oldStats.speed) != 0)
        {
            float diff = newStats.speed - oldStats.speed;
            changes.Add($"+{diff:F1}% Speed");
        }
        if (Mathf.Abs(newStats.amount - oldStats.amount) != 0)
        {
            int diff = newStats.amount - oldStats.amount;
            changes.Add($"+{diff} Projectiles");
        }
        if (Mathf.Abs(newStats.cooldown - oldStats.cooldown) != 0)
        {
            float diff = oldStats.cooldown - newStats.cooldown;
            changes.Add($"{diff:F1}% Cooldown");
        }
        if (Mathf.Abs(newStats.growth - oldStats.growth) != 0)
        {
            int diff = newStats.growth - oldStats.growth;
            changes.Add($"+{diff:F1}% Exp Gain");
        }
        if (Mathf.Abs(newStats.attractionRange - oldStats.attractionRange) != 0)
        {
            float diff = newStats.attractionRange - oldStats.attractionRange;
            changes.Add($"+{diff:F1}% Attraction Range");
        }

        // Join all improvements into one line
        return changes.Count > 0
            ? string.Join(", ", changes)
            : "No stat change";
    }
}
