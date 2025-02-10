using System;

[Serializable]
public struct PlayerStats
{
    public int maxHealth;
    public float recovery; // Health regen per second
    public float moveSpeed;
    public float attractionRange;
    public int armor; // Damage reduction (flat)
    public int growth; // exp gain increase (%)
}
