using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool
{
    readonly Queue<GameObject> _activeObjects = new Queue<GameObject>();
    readonly Queue<GameObject> _inactiveObjects = new Queue<GameObject>();
    readonly GameObject _prefab;

    public ObjectPool(GameObject prefab, int initialCount)
    {
        _prefab = prefab;
        Prewarm(initialCount);
    }

    void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = GameObject.Instantiate(_prefab, Vector3.zero, Quaternion.identity, null);
            _inactiveObjects.Enqueue(obj);
            obj.SetActive(false);
        }
    }

    public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject obj;
        if (_inactiveObjects.Count > 0)
        {
            obj = _inactiveObjects.Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.SetParent(parent);
            obj.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(_prefab, position, rotation, parent);
        }
        _activeObjects.Enqueue(obj);
        return obj;
    }

    public void Despawn(GameObject gameObject)
    {
        if (_activeObjects.Contains(gameObject))
        {
            _activeObjects.Dequeue();
            _inactiveObjects.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Trying to despawn an object that is not active in this pool", gameObject);
        }
    }
}
