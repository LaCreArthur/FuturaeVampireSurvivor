using UnityEngine;

public class AxeWeapon : Weapon
{
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] GameObject projectilePrefab;

    public override void Fire(GameObject attacker)
    {
        Vector2 worldSpawnPosition = transform.TransformPoint(spawnOffset);
        GameObject projectile = PoolManager.Spawn(projectilePrefab, worldSpawnPosition, Quaternion.identity);

        // scale the axe based on area stat
        projectile.transform.localScale = Vector3.one * modifiedStats.area;

        var projController = projectile.GetComponent<AxeProjectileController>();
        if (projController != null)
        {
            projController.Initialize(modifiedStats.speed, modifiedStats.damage, attacker);
        }
    }
}
