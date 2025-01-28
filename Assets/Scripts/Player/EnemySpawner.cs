using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     Should scale to screen size and be used to spawn enemies on the bounds of the screen.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] int maxSpawned;
    [SerializeField] int spawnRate;
    [SerializeField] float borderSize = 2f;

    Camera _cam;
    float _nextSpawnTime;

    void Awake() => _cam = Camera.main;
    void Start() => GameStateManager.OnStateChange += OnStateChanged;


    void Update()
    {
        _nextSpawnTime -= Time.deltaTime;
        if (_nextSpawnTime <= 0)
        {
            Spawn();
            _nextSpawnTime = spawnRate;
        }
    }
    void OnDestroy() => GameStateManager.OnStateChange -= OnStateChanged;

    void OnDrawGizmos()
    {
        if (_cam == null) return;

        float verticalOffset = _cam.orthographicSize + borderSize;
        float horizontalOffset = verticalOffset * _cam.aspect;

        // Drawing the spawn boundary
        Vector3 topLeft = _cam.transform.TransformPoint(new Vector3(-horizontalOffset, verticalOffset, 0));
        Vector3 topRight = _cam.transform.TransformPoint(new Vector3(horizontalOffset, verticalOffset, 0));
        Vector3 bottomLeft = _cam.transform.TransformPoint(new Vector3(-horizontalOffset, -verticalOffset, 0));
        Vector3 bottomRight = _cam.transform.TransformPoint(new Vector3(horizontalOffset, -verticalOffset, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;

    void Spawn()
    {
        Vector3 position = transform.position + GetRandomSpawnPosition();
        PoolManager.Spawn(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], position, Quaternion.identity);
    }


    Vector3 GetRandomSpawnPosition()
    {
        // Get viewport dimensions
        float verticalOffset = _cam.orthographicSize + borderSize;
        float horizontalOffset = verticalOffset * _cam.aspect;

        // Randomly choose which side to spawn (0: top, 1: right, 2: bottom, 3: left)
        int side = Random.Range(0, 4);

        float x, y;
        switch (side)
        {
            case 0: // Top
                x = Random.Range(-horizontalOffset, horizontalOffset);
                y = verticalOffset;
                break;
            case 1: // Right
                x = horizontalOffset;
                y = Random.Range(-verticalOffset, verticalOffset);
                break;
            case 2: // Bottom
                x = Random.Range(-horizontalOffset, horizontalOffset);
                y = -verticalOffset;
                break;
            default: // Left
                x = -horizontalOffset;
                y = Random.Range(-verticalOffset, verticalOffset);
                break;
        }

        return new Vector3(x, y, 0);
    }
}
