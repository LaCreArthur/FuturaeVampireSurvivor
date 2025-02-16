using UnityEngine;

public class EnemyHealthSystem : HealthSystem, IPoolable
{
    [SerializeField] float knockbackFactor;

    PickableSpawner pickableSpawner;

    void Awake()
    {
        pickableSpawner = GetComponent<PickableSpawner>();
        GameStateManager.OnGameOver += Despawn;
    }

    void OnDestroy() => GameStateManager.OnGameOver -= Despawn;

    public void OnSpawn() => CurrentHp = MaxHp;

    public void TakeDamage(int amount, float knockback = 0)
    {
        base.TakeDamage(amount);
        if (knockback > 0)
        {
            ApplyKnockback(knockback);
        }
        if (CurrentHp <= 0)
        {
            pickableSpawner.SpawnPickable();
            Despawn();
        }
    }
    void Despawn() => PoolManager.Despawn(gameObject);

    void ApplyKnockback(float knockback)
    {
        // get the direction from the attacker to the enemy
        Vector2 knockbackDirection = (transform.position - PlayerTransform.Value.position).normalized;
        // apply force in the opposite direction of the attacker
        transform.position += (Vector3)knockbackDirection * knockback * knockbackFactor;
    }
}
