public class PowerUp : Upgradable<Modifiers>
{
    public Modifiers AddModifier(Modifiers pStats)
    {
        pStats.maxHealth += stats.maxHealth;
        pStats.recovery += stats.recovery;
        pStats.armor += stats.armor;
        pStats.moveSpeed += stats.moveSpeed;
        pStats.might += stats.might;
        pStats.area += stats.area;
        pStats.speed -= stats.speed;
        pStats.amount += stats.amount;
        pStats.cooldown -= stats.cooldown;
        pStats.growth += stats.growth;
        pStats.attractionRange += stats.attractionRange;
        return pStats;
    }
}
