public class EnemyHealthSystem : HealthSystem, IPoolable
{
    public void OnSpawn() => CurrentHealth = MaxHealth = maxHealth;

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (CurrentHealth <= 0)
        {
            PoolManager.Despawn(gameObject);
        }
    }
}
