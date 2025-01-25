using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     Should scale to screen size and be used to spawn enemies on the bounds of the screen.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<EnemySO> enemySOs;
    [SerializeField] int maxSpawned;
    [SerializeField] int spawnRate;
    [SerializeField] float borderSize = 2f;

    Camera _cam;
    float _nextSpawnTime;

    void Awake() => _cam = Camera.main;



    void Update()
    {
        _nextSpawnTime -= Time.deltaTime;
        if (_nextSpawnTime <= 0)
        {
            Spawn();
            _nextSpawnTime = spawnRate;
        }
    }

    void Spawn()
    {
        Vector3 position = GetRandomSpawnPosition();
        enemySOs[Random.Range(0, enemySOs.Count)].CreateEnemy(position, Quaternion.identity);
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
