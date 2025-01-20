/// <summary>
///     Use this interface to define logics for an object when it is spawned or despawned from a pool.
/// </summary>
public interface IPoolable
{
    public void OnSpawn();
    public void OnDespawn();
}
