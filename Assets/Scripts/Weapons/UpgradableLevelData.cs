using System;

[Serializable]
public struct UpgradableLevelData
{
    public int damage;
    public float cooldown; // Seconds
    public float area; // AoE radius or scale
    public float speed; // Projectile speed or attack speed
    public int amount; // Number of projectiles
    public float duration; // Seconds
    public int pierce; // Number of enemies or collisions a projectile can pass through
    public float projectileInterval; // Time between multiple projectiles in a single attack
    public float knockback;

    // Additional stats for power-ups
    public int maxHealth;
    public int recovery;
    public float moveSpeed;
    public int armor;
    public float attractionRange;
    public int growth;
}

//todo: player script to hold it stats
//and player logic to apply the stats (maxHealth, recovery, moveSpeed, armor, attractionRange, growth)
//and refactor poweredUpStats out of weapon, to have every power ups applied to the player stats
//then when using weapon stats, also look for player stats and apply them
