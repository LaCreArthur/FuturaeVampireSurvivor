public class DespawnDeathBehavior : DeathBehavior
{
    public override void Die() => PoolManager.Despawn(gameObject);
}
