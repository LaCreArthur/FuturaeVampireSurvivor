using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttack", menuName = "Attack Behaviors/RangedAttack")]
public class RangedAttackBehaviorSO : AttackBehaviorSO
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] Vector3 spawnOffset;

    public override void ExecuteAttack(GameObject attacker)
    {
        GameObject projectile = PoolManager.Spawn(projectilePrefab, attacker.transform.position + spawnOffset, attacker.transform.rotation);
        var projController = projectile.GetComponent<ProjectileController>();
        if (projController != null)
        {
            projController.Initialize(projectileSpeed, damage, attacker);
        }
    }
}
