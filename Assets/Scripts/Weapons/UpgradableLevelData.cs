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
}
