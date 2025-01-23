using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttack", menuName = "Attack Behaviors/RangedAttack")]
public class RangedAttackSO : AttackBehaviorSO
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Vector3 spawnOffset;

    public override void ExecuteAttack(GameObject attacker)
    {
        // Instantiate a projectile and give it a direction, 
        // using the AttackData to set damage or speed
        GameObject projectile = PoolManager.Spawn(projectilePrefab, attacker.transform.position + spawnOffset, attacker.transform.rotation);
        var projController = projectile.GetComponent<ProjectileController>();
        if (projController != null)
        {
            projController.Initialize(speed, damage, attacker);
        }
    }
}
