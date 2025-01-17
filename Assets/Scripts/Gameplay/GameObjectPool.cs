using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    readonly List<GameObject> _activeObjects = new List<GameObject>();
    readonly List<GameObject> _inactiveObjects = new List<GameObject>();
    // Keep a reference to the IPoolable components of each object so we don't call GetComponents every time
    readonly Dictionary<GameObject, IPoolable[]> _poolableComponents = new Dictionary<GameObject, IPoolable[]>();
    readonly GameObject _prefab;

    public GameObjectPool(GameObject prefab, int initialCount)
    {
        _prefab = prefab;
        Prewarm(initialCount);
    }

    // Pre-instantiate a set number of objects and deactivate them
    void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Object.Instantiate(_prefab);
            _poolableComponents[go] = go.GetComponents<IPoolable>();
            go.SetActive(false);
            _inactiveObjects.Add(go);
        }
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject go;

        // Reuse an existing inactive object if available
        if (_inactiveObjects.Count > 0)
        {
            int lastIndex = _inactiveObjects.Count - 1;
            go = _inactiveObjects[lastIndex];
            _inactiveObjects.RemoveAt(lastIndex);

            // Re-parent and reposition before activating
            if (parent != null)
                go.transform.SetParent(parent, false);
            // Assign position and rotation
            go.transform.SetPositionAndRotation(position, rotation);
            go.SetActive(true);
        }
        else
        {
            // Instantiate a fresh object if none are inactive
            go = Object.Instantiate(_prefab, position, rotation, parent);
            _poolableComponents[go] = go.GetComponents<IPoolable>();
        }

        _activeObjects.Add(go);
        // call OnSpawn on IPoolable components of the object
        foreach (IPoolable p in _poolableComponents[go])
        {
            p.OnSpawn();
        }

        return go;
    }

    public void Despawn(GameObject go)
    {
        if (_activeObjects.Remove(go))
        {
            go.SetActive(false);
            go.transform.SetParent(null);
            _inactiveObjects.Add(go);
        }
        else
        {
            Debug.LogWarning("Trying to despawn a game object that is not in the active pool.", go);
        }
    }
}
