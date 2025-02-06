using UnityEngine;

public class MagicWandWeapon : Weapon
{
    [SerializeField] float nearestEnemyRadius;
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] GameObject projectilePrefab;

    readonly Collider2D[] _enemiesInRange = new Collider2D[16];
    ContactFilter2D _contactFilter;

    protected override void Awake()
    {
        base.Awake();
        _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(LayerMask.GetMask("Enemies"));
        _contactFilter.useLayerMask = true;
    }

    public override void Fire(GameObject attacker)
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
            projController.Initialize(PoweredUpStats.speed, PoweredUpStats.damage, attacker, direction);
        }
    }

    bool FindNearestUsingOverlapSphere(out Transform nearestEnemy)
    {
        int size = Physics2D.OverlapCircle(transform.position, nearestEnemyRadius, _contactFilter, _enemiesInRange);
        float minDist = float.MaxValue;
        nearestEnemy = null;

        for (int i = 0; i < size; i++)
        {
            Collider2D hit = _enemiesInRange[i];
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestEnemy = hit.transform;
            }
        }
        return nearestEnemy != null;
    }
}
