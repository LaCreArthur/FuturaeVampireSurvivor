using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    static readonly Dictionary<GameObject, GameObjectPool> Pools = new Dictionary<GameObject, GameObjectPool>();

    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!Pools.ContainsKey(prefab))
        {
            Pools[prefab] = new GameObjectPool(prefab, 10);
        }
        return Pools[prefab].Spawn(position, rotation, parent);
    }

    public static void Despawn(GameObject prefab, GameObject instance)
    {
        if (Pools.TryGetValue(prefab, out GameObjectPool pool))
        {
            pool.Despawn(instance);
        }
        else
        {
            Debug.LogWarning("Trying to despawn a game object that is not in the pool.", instance);
        }
    }
}
