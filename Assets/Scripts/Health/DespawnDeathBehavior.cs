using UnityEngine;

public class DespawnDeathBehavior : DeathBehavior
{
    public override void Die(GameObject owner) => PoolManager.Despawn(owner);
}
