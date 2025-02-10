using Unity.Mathematics;
using UnityEngine;

public class ExperienceOrbSpawner : MonoBehaviour, IPoolable
{
    [SerializeField] GameObject orbPrefab;

    bool _isSpawned;
    bool _isQuitting;

    void OnDisable()
    {
        if (_isSpawned && !_isQuitting) PoolManager.Spawn(orbPrefab, transform.position, quaternion.identity);
        _isSpawned = false;
    }

    void OnApplicationQuit() => _isQuitting = true;

    public void OnSpawn() => _isSpawned = true;
}
