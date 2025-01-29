public class DespawnDeathBehavior : DeathBehavior
{
    public override void OnDeath() => PoolManager.Despawn(gameObject);
}
