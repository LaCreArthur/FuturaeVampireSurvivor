using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickableSpawner : MonoBehaviour, IPoolable
{
    [SerializeField] GameObject[] pickables;
    [SerializeField] [Range(0, 1)] float probability = 1f;
    bool _isQuitting;

    public bool IsSpawned { get; private set; }

    void OnApplicationQuit() => _isQuitting = true;

    public void OnSpawn() => IsSpawned = true;

    public void SpawnPickable()
    {
        if (IsSpawned && !_isQuitting && Random.value < probability)
        {
            PoolManager.Spawn(pickables.GetRandom(), transform.position, quaternion.identity);
        }
        IsSpawned = false;
    }
}
