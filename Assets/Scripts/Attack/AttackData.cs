using UnityEngine;

public abstract class AttackBehaviorSO : ScriptableObject
{
    public int damage = 10;
    public float range = 1f;
    public float areaRadius = 2f;
    public float cooldown = 1f;
    public float speed = 1f;

    public abstract void ExecuteAttack(GameObject attacker);
}
