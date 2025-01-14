using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Distance between consecutive segments on the X-axis
    const float NEXT_SEGMENT_DISTANCE = 30f;

    [Header("Prefabs")]
    [SerializeField] List<GameObject> segmentPrefabs;
    [SerializeField] Transform segmentParent;

    [Header("Spawning Parameters")]
    [SerializeField] float spawnDistance = 10f;
    [SerializeField] float despawnDistance = -10f;

    // Queue to keep track of active segments in FIFO order and easily despawn the oldest one
    readonly Queue<ActiveSegment> _activeSegments = new Queue<ActiveSegment>();

    // Reference to the last spawned segment's transform to calculate the next spawn position
    Transform _lastSegment;

    // Reference to the player's transform to know when to spawn new segments
    Transform _playerTransform;

    // Dictionary to manage object pools for each prefab
    Dictionary<GameObject, ObjectPool> _objectPools;

    // Property shortcut to get the X position of the last segment
    float LastSegmentX => _lastSegment?.position.x ?? 0;

    // Property shortcut to get the player's current X position
    float PlayerX => _playerTransform?.position.x ?? 0f;


    void Start()
    {
        InitializePools();
        _playerTransform = transform; // this script is attached to the player
        GameStateManager.OnHome += ResetLevel;
        ResetLevel();
    }

    void Update()
    {
        HandleSegmentSpawning();
        DespawnOldestSegment();
    }

    void OnDestroy() => GameStateManager.OnHome -= ResetLevel;

    void InitializePools()
    {
        _objectPools = new Dictionary<GameObject, ObjectPool>();
        foreach (GameObject prefab in segmentPrefabs)
        {
            _objectPools[prefab] = new ObjectPool(prefab, 3);
        }
    }

    void ResetLevel()
    {
        _lastSegment = null;
        DespawnAllSegments();
        GenerateInitialLevel();
    }

    void GenerateInitialLevel()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnNextSegment();
        }
    }

    void SpawnNextSegment()
    {
        float nextSegmentX = LastSegmentX + NEXT_SEGMENT_DISTANCE;

        // Randomly select a segment prefab from the list and get its pool
        GameObject selectedPrefab = segmentPrefabs[Random.Range(0, segmentPrefabs.Count)];

        ObjectPool pool = _objectPools[selectedPrefab];

        // Spawn a new segment from the pool
        GameObject nextSegment = pool.Spawn(new Vector3(nextSegmentX, 0f, 0f), Quaternion.identity, segmentParent);

        // Enqueue the new segment as active
        _activeSegments.Enqueue(new ActiveSegment(selectedPrefab, nextSegment));

        // Update the reference to the last segment
        _lastSegment = nextSegment.transform;
    }

    void HandleSegmentSpawning()
    {
        if (LastSegmentX < PlayerX + spawnDistance)
        {
            SpawnNextSegment();
        }
    }

    void DespawnOldestSegment()
    {
        if (_activeSegments.Count == 0) return;

        // Peek at the oldest active segment
        ActiveSegment oldestSegment = _activeSegments.Peek();

        if (oldestSegment.Instance.transform.position.x < PlayerX + despawnDistance)
        {
            // Despawn the segment back to its pool
            _objectPools[oldestSegment.Prefab].Despawn(oldestSegment.Instance);

            // Dequeue the segment from the active queue
            _activeSegments.Dequeue();
        }
    }

    [ContextMenu("Despawn All Segments")]
    void DespawnAllSegments()
    {
        while (_activeSegments.Count > 0)
        {
            ActiveSegment segment = _activeSegments.Dequeue();
            _objectPools[segment.Prefab].Despawn(segment.Instance);
        }
    }
}

public class ActiveSegment
{
    public ActiveSegment(GameObject prefab, GameObject instance)
    {
        Prefab = prefab;
        Instance = instance;
    }
    public GameObject Prefab { get; }
    public GameObject Instance { get; }
}
