using System;

[Serializable]
public struct WeaponStats
{
    public int damage;
    public float cooldown; // Seconds
    public float area; // AoE radius or scale
    public float speed; // Projectile speed or attack speed
    public float duration; // Seconds
    public float knockback; // backwards force applied to enemies on hit
    public float projectileInterval; // Time between multiple projectiles in a single attack
    public int amount; // Number of projectiles
    public int pierce; // Number of enemies or collisions a projectile can pass through
}
