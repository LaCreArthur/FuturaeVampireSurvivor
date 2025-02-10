public class PowerUp : Upgradable<PowerUpStats>
{
    public WeaponStats ApplyWeaponPowerUp(WeaponStats wStats)
    {
        wStats.damage += stats.weaponStats.damage;
        wStats.cooldown -= stats.weaponStats.cooldown;
        wStats.area += stats.weaponStats.area;
        wStats.speed += stats.weaponStats.speed;
        wStats.amount += stats.weaponStats.amount;
        wStats.duration += stats.weaponStats.duration;
        wStats.pierce += stats.weaponStats.pierce;
        wStats.projectileInterval -= stats.weaponStats.projectileInterval;
        wStats.knockback += stats.weaponStats.knockback;
        return wStats;
    }

    public PlayerStats ApplyPlayerPowerUp(PlayerStats pStats)
    {
        pStats.maxHealth += stats.playerStats.maxHealth;
        pStats.recovery += stats.playerStats.recovery;
        pStats.moveSpeed += stats.playerStats.moveSpeed;
        pStats.armor += stats.playerStats.armor;
        pStats.attractionRange += stats.playerStats.attractionRange;
        pStats.growth += stats.playerStats.growth;
        return pStats;
    }
}
