using UnityEngine;

public class KnifeWeapon : UpgradableBehavior
{
    [SerializeField] Vector3 spawnOffset;
    [SerializeField] GameObject projectilePrefab;
    Vector2 _lastInput;

    void Update()
    {
        // Save the last input direction to use as the knife's direction if the player is not moving
        // Must be done in Update and not Fire because the player can fire the knife while not moving
        // And we want the knife to be fired in the last direction the player was moving
        if (InputManager.Input.sqrMagnitude > InputManager.DEAD_ZONE)
            _lastInput = InputManager.Input;
    }

    public override void Fire(GameObject attacker)
    {
        Vector2 worldSpawnPosition = transform.TransformPoint(spawnOffset);

        float angle = Mathf.Atan2(_lastInput.y, _lastInput.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);

        GameObject projectile = PoolManager.Spawn(projectilePrefab, worldSpawnPosition, rotation);
        var projController = projectile.GetComponent<KnifeProjectileController>();
        if (projController != null)
        {
            projController.Initialize(Stats.speed, Stats.damage, attacker);
        }
    }
}
