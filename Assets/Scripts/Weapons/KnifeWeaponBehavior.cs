using UnityEngine;

public class KnifeWeaponBehavior : WeaponBehavior
{
    [SerializeField] GameObject knifePrefab;
    [SerializeField] float knifeSpeed;
    [SerializeField] Vector3 spawnOffset;

    public override void ExecuteAttack(GameObject attacker)
    {
        Vector2 worldSpawnPosition = transform.TransformPoint(spawnOffset);

        Vector2 input = InputManager.Input;
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);

        GameObject projectile = PoolManager.Spawn(knifePrefab, worldSpawnPosition, rotation);
        var projController = projectile.GetComponent<KnifeProjectileController>();
        if (projController != null)
        {
            projController.Initialize(knifeSpeed, weaponData.damage, attacker);
        }
    }
}
