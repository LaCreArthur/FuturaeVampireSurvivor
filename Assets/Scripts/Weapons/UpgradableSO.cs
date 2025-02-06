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
        if (weaponLevel == -1 || isPowerUp)
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

        if (Mathf.Abs(newData.damage - oldData.damage) != 0)
        {
            int diff = newData.damage - oldData.damage;
            changes.Add($"+{diff}{(isPowerUp ? "%" : "")} Damage");
        }

        if (Mathf.Abs(newData.cooldown - oldData.cooldown) != 0)
        {
            float diff = oldData.cooldown - newData.cooldown;
            changes.Add($"-{diff:F1}{(isPowerUp ? "%" : "s")} Cooldown");
        }

        if (Mathf.Abs(newData.area - oldData.area) != 0)
        {
            float diff = newData.area - oldData.area;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "")} Area");
        }

        if (Mathf.Abs(newData.amount - oldData.amount) != 0)
        {
            int diff = newData.amount - oldData.amount;
            changes.Add($"+{diff} Projectiles");
        }

        if (Mathf.Abs(newData.duration - oldData.duration) != 0)
        {
            float diff = newData.duration - oldData.duration;
            changes.Add($"+{diff:F1}{(isPowerUp ? "%" : "s")} Duration");
        }

        if (Mathf.Abs(newData.pierce - oldData.pierce) != 0)
        {
            int diff = newData.pierce - oldData.pierce;
            changes.Add($"+{diff} Pierce");
        }

        if (Mathf.Abs(newData.projectileInterval - oldData.projectileInterval) != 0)
        {
            float diff = oldData.projectileInterval - newData.projectileInterval;
            changes.Add($"-{diff:F1}{(isPowerUp ? "%" : "s")} Between Projectiles");
        }

        if (Mathf.Abs(newData.knockback - oldData.knockback) != 0)
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
