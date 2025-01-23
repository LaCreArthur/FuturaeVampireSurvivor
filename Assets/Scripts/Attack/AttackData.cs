using UnityEngine;

public abstract class AttackBehaviorSO : ScriptableObject
{
    public float damage = 10f;
    public float range = 1f;
    public float areaRadius = 2f;
    public float cooldown = 1f;
    public float speed = 1f;

    public abstract void ExecuteAttack(GameObject attacker);
}
