using System;

[Serializable]
public struct Modifiers
{
    // player specific
    public int maxHealth; // Increase max health (%)
    public float recovery; // Health regen per second
    public int armor; // Damage reduction (flat)
    public float moveSpeed; // %
    public int growth; // Exp gain increase (%)
    public float attractionRange; // item attraction range (%)
    // shared with weapons
    public float might; // Increase all damages (%)
    public float area; // AoE radius or scale (%)
    public float speed; // Projectile speed or attack speed (% seconds)
    public int amount; // Number of projectiles (flat)
    public float cooldown; // % seconds
}
