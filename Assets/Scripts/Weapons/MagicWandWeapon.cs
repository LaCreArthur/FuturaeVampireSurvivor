using System.Collections;
using UnityEngine;

public class MagicWandWeapon : Weapon
{
    [SerializeField] float nearestEnemyRadius;
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] GameObject projectilePrefab;

    readonly Collider2D[] _enemiesInRange = new Collider2D[4];
    ContactFilter2D _contactFilter;

    protected override void Awake()
    {
        base.Awake();
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(LayerMask.GetMask("Enemies"));
        _contactFilter.useLayerMask = true;
    }

    public override void Fire(GameObject attacker) => StartCoroutine(SpawnMagicMissilesRoutine(attacker));

    IEnumerator SpawnMagicMissilesRoutine(GameObject attacker)
    {
        for (int i = 0; i < modifiedStats.amount; i++)
        {
            SpawnMagicMissile(attacker);
            yield return new WaitForSeconds(modifiedStats.projectileInterval);
        }
    }

    void SpawnMagicMissile(GameObject attacker)
    {
        Vector2 worldSpawnPosition = transform.TransformPoint(spawnOffset);
        GameObject projectile = PoolManager.Spawn(projectilePrefab, worldSpawnPosition, transform.rotation);
        var projController = projectile.GetComponent<MagicMissileController>();
        if (projController != null)
        {
            Vector3 direction;
            if (FindNearestUsingOverlapSphere(out Transform nearestEnemy))
            {
                // Enemy found, set missile direction towards the enemy
                direction = (nearestEnemy.position - transform.position).normalized;
            }
            else
            {
                // No enemy found, set missile to a random direction
                direction = Random.insideUnitSphere.normalized;
            }
            direction.z = 0;
            projController.Initialize(modifiedStats.speed, modifiedStats.damage, attacker, direction);
        }
    }

    bool FindNearestUsingOverlapSphere(out Transform nearestEnemy)
    {
        Physics2D.OverlapCircle(transform.position, nearestEnemyRadius, _contactFilter, _enemiesInRange);
        nearestEnemy = _enemiesInRange.GetRandom()?.transform;
        return nearestEnemy != null;
    }
}
