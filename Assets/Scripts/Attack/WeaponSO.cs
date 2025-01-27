using UnityEngine;

public abstract class WeaponSO : ScriptableObject
{
    public int damage = 10;
    public float cooldown = 1f;

    public abstract void ExecuteAttack(GameObject attacker);
}
