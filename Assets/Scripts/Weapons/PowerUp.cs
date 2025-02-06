using UnityEngine;

public class PowerUp : Upgradable
{
    public UpgradableLevelData Apply(UpgradableLevelData data)
    {
        UpgradableLevelData poweredUpData = data;
        poweredUpData.damage += Mathf.RoundToInt(data.damage * stats.damage / 100f);
        poweredUpData.cooldown -= data.cooldown * stats.cooldown / 100f;
        poweredUpData.area += data.area * stats.area / 100f;
        poweredUpData.speed += data.speed * stats.speed / 100f;
        poweredUpData.amount += stats.amount;
        poweredUpData.duration += data.duration * stats.duration / 100f;
        poweredUpData.pierce += stats.pierce;
        poweredUpData.projectileInterval += data.projectileInterval * stats.projectileInterval / 100f;
        poweredUpData.knockback += data.knockback * stats.knockback / 100f;
        return poweredUpData;
    }
}
