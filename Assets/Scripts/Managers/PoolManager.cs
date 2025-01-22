using System.Collections.Generic;
using UnityEngine;

public static class PoolManager
{
    static readonly Dictionary<GameObject, GameObjectPool> Pools = new Dictionary<GameObject, GameObjectPool>();

    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!Pools.ContainsKey(prefab))
        {
            Pools[prefab] = new GameObjectPool(prefab, 3);
        }
        return Pools[prefab].Spawn(position, rotation, parent);
    }

    public static void Despawn(GameObject instance)
    {
        if (instance.TryGetComponent(out PoolablePrefabRef prefabRef))
        {
            if (prefabRef.Prefab == null)
            {
                Debug.LogWarning($"the PoolablePrefabRef component has a null prefab ref. (object: {instance.name}", instance);
                return;
            }
            if (Pools.TryGetValue(prefabRef.Prefab, out GameObjectPool pool))
                pool.Despawn(instance);
            else
                Debug.LogWarning($"Trying to despawn a game object that is not in the pool. (object: {instance.name}, pool: {prefabRef.Prefab.name}", instance);
        }
        else
            Debug.LogWarning($"Trying to despawn a game object that has not a PoolablePrefabRef component. (object: {instance.name}", instance);
    }
}
