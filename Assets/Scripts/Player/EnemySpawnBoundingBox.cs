using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     Should scale to screen size and be used to spawn enemies on the bounds of the screen.
/// </summary>
public class EnemySpawnBoundingBox : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int maxSpawned;
    [SerializeField] int spawnRate;
    [SerializeField] float borderSize = 2f;

    Camera _cam;
    float _nextSpawnTime;
    float SpawnInterval => 1f / spawnRate;
    Vector2 Min { get; set; }

    Vector2 Max { get; set; }

    void Awake()
    {
        _cam = Camera.main;
        _nextSpawnTime = Time.time + SpawnInterval;
    }



    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            Spawn();
            _nextSpawnTime = Time.time + SpawnInterval;
        }
    }

    void Spawn()
    {
        Vector3 position = GetRandomSpawnPosition();
        PoolManager.Spawn(prefab, position, Quaternion.identity, null);
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

        DebugBounds(horizontalOffset, verticalOffset);
        Vector3 worldCamPos = _cam.transform.TransformPoint(new Vector3(x, y, 0));
        worldCamPos.y = 0.3f;
        return worldCamPos;
    }

    void DebugBounds(float horizontalOffset, float verticalOffset)
    {
        Vector3 topLeft = _cam.transform.TransformPoint(new Vector3(-horizontalOffset, verticalOffset, 0));
        Vector3 topRight = _cam.transform.TransformPoint(new Vector3(horizontalOffset, verticalOffset, 0));
        Vector3 bottomRight = _cam.transform.TransformPoint(new Vector3(horizontalOffset, -verticalOffset, 0));
        Vector3 bottomLeft = _cam.transform.TransformPoint(new Vector3(-horizontalOffset, -verticalOffset, 0));

        topLeft.y = 0.5f;
        topRight.y = 0.5f;
        bottomRight.y = 0.5f;
        bottomLeft.y = 0.5f;

        // Draw the rectangle
        Debug.DrawLine(topLeft, topRight, Color.red, Time.deltaTime);
        Debug.DrawLine(topRight, bottomRight, Color.red, Time.deltaTime);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, Time.deltaTime);
        Debug.DrawLine(bottomLeft, topLeft, Color.red, Time.deltaTime);
    }
}
