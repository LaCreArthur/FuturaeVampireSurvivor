using UnityEngine;

public class EnemyHealthSystem : HealthSystem, IPoolable
{
    [SerializeField] float knockbackFactor;

    PickableSpawner _pickableSpawner;

    float _baseMaxHp;

    void Awake()
    {
        _pickableSpawner = GetComponent<PickableSpawner>();
        GameStateManager.OnGameOver += Despawn;
        _baseMaxHp = MaxHp;
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
            _pickableSpawner.SpawnPickable();
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

    public void IncreaseHp(float percentage)
    {
        maxHp = _baseMaxHp + _baseMaxHp * percentage;
        CurrentHp = MaxHp;
    }
}
