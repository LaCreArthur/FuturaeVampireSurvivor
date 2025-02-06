﻿public class EnemyHealthSystem : HealthSystem, IPoolable
{
    public void OnSpawn() => CurrentHp = MaxHp;

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        if (CurrentHp <= 0)
        {
            PoolManager.Despawn(gameObject);
        }
    }
}
