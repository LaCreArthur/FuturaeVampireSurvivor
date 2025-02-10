using UnityEngine;

public class PowerUp : Upgradable
{
    [SerializeField] PlayerStats playerModifier;

    public WeaponStats ApplyWeaponPowerUp(WeaponStats wStats)
    {
        wStats.damage += stats.damage;
        wStats.cooldown -= stats.cooldown;
        wStats.area += stats.area;
        wStats.speed += stats.speed;
        wStats.amount += stats.amount;
        wStats.duration += stats.duration;
        wStats.pierce += stats.pierce;
        wStats.projectileInterval -= stats.projectileInterval;
        wStats.knockback += stats.knockback;
        return wStats;
    }

    public PlayerStats ApplyPlayerPowerUp(PlayerStats pStats)
    {
        pStats.maxHealth += playerModifier.maxHealth;
        pStats.recovery += playerModifier.recovery;
        pStats.moveSpeed += playerModifier.moveSpeed;
        pStats.armor += playerModifier.armor;
        pStats.attractionRange += playerModifier.attractionRange;
        pStats.growth += playerModifier.growth;
        return pStats;
    }
}
