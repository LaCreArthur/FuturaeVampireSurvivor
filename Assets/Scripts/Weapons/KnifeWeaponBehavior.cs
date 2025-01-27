using UnityEngine;

public class KnifeWeaponBehavior : WeaponBehavior
{
    [SerializeField] GameObject knifePrefab;
    [SerializeField] float knifeSpeed;
    [SerializeField] Vector3 spawnOffset;

    public override void ExecuteAttack(GameObject attacker)
    {
        Vector2 worldSpawnPosition = attacker.transform.TransformPoint(spawnOffset);
        GameObject projectile = PoolManager.Spawn(knifePrefab, worldSpawnPosition, attacker.transform.rotation);
        var projController = projectile.GetComponent<KnifeProjectileController>();
        if (projController != null)
        {
            projController.Initialize(knifeSpeed, weaponData.damage, attacker);
        }
    }
}
