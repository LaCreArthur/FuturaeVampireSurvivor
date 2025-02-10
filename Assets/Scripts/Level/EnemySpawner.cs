using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<SpawnWaveSO> spawnWaves;
    [SerializeField] int maxSpawned;
    [SerializeField] float spawnRate;
    [SerializeField] float borderSize = 2f;

    Camera _cam;
    float _nextSpawnTime;
    int _waveIndex;
    SpawnWaveSO _currentWave;
    bool _burst;
    Vector3 _burstSpawnPosition;

    void Awake()
    {
        GameStateManager.OnStateChange += OnStateChanged;
        enabled = false;
        _cam = Camera.main;
    }

    void OnEnable()
    {
        _waveIndex = 0;
        SetNextWave();
    }

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

    void SetNextWave()
    {
        _currentWave = spawnWaves[_waveIndex];
        _waveIndex = (_waveIndex + 1) % spawnWaves.Count; // Loop back to the first wave if we reach the end
        maxSpawned = _currentWave.maxSpawned;
        spawnRate = _currentWave.spawnRate;
        _burst = _currentWave.burst;
        if (_currentWave.burst)
        {
            // get a random spawn position for the burst
            _burstSpawnPosition = transform.position + GetRandomSpawnPosition();
        }
    }
    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;

    void Spawn()
    {
        // if burst, spawn all enemies at the same time and position
        if (_burst)
        {
            for (int i = 0; i < maxSpawned; i++)
            {
                SpawnEnemy(_burstSpawnPosition + new Vector3(Random.value, Random.value));
            }
            // move to the next wave directly
            SetNextWave();
        }
        else
        {
            if (maxSpawned > 0)
            {
                SpawnEnemy(transform.position + GetRandomSpawnPosition());
                maxSpawned--;
            }
            else
            {
                // if no more enemies to spawn, move to the next wave
                SetNextWave();
            }
        }
    }

    void SpawnEnemy(Vector3 position) => PoolManager.Spawn(_currentWave.enemies[Random.Range(0, _currentWave.enemies.Count)], position, Quaternion.identity);

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
