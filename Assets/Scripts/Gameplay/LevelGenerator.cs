using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // Distance between consecutive segments on the X-axis
    const float NEXT_SEGMENT_DISTANCE = 30f;

    [Header("Prefabs")]
    [SerializeField] List<GameObject> segmentPrefabs;

    [Header("Spawning Parameters")]
    [SerializeField] float spawnDistance = 10f;
    [SerializeField] float despawnDistance = -10f;

    // Queue to keep track of active segments in FIFO order and easily despawn the oldest one
    readonly Queue<ActiveSegment> _activeSegments = new Queue<ActiveSegment>();

    // Reference to the last spawned segment's transform to calculate the next spawn position
    Transform _lastSegment;

    // Reference to the player's transform to know when to spawn new segments
    Transform _playerTransform;

    // Property shortcut to get the X position of the last segment
    float LastSegmentX => _lastSegment?.position.x ?? 0;

    // Property shortcut to get the player's current X position
    float PlayerX => _playerTransform?.position.x ?? 0f;


    void Start()
    {
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

    void ResetLevel()
    {
        _lastSegment = null;
        DespawnAllSegments();
        // delay the generation of the initial level to avoid despawning the physic objects in the same frame of the GameStateManager.OnHome event
        DOVirtual.DelayedCall(0.1f, GenerateInitialLevel);
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

        // Spawn a new segment from the pool
        GameObject nextSegment = PoolManager.Spawn(selectedPrefab, new Vector3(nextSegmentX, 0f, 0f), Quaternion.identity, null);

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
            PoolManager.Despawn(oldestSegment.Prefab, oldestSegment.Instance);

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
            PoolManager.Despawn(segment.Prefab, segment.Instance);
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
