using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public float cellSize;
    public GameObject[] cellPrefab;

    readonly Dictionary<Vector2Int, GameObject> _cells = new Dictionary<Vector2Int, GameObject>(32);
    readonly HashSet<Vector2Int> _removalSet = new HashSet<Vector2Int>(32);

    readonly Vector2Int[] _spawnPattern =
    {
        // Adjacent cells
        new Vector2Int(0, 0), // center
        new Vector2Int(0, 1), // right
        new Vector2Int(1, 1), // top right
        new Vector2Int(1, 0), // top
        new Vector2Int(1, -1), // top left
        new Vector2Int(0, -1), // left
        new Vector2Int(-1, -1), // bottom left
        new Vector2Int(-1, 0), // bottom
        new Vector2Int(-1, 1), // bottom right
    };

    Vector2Int _playerGridPos;
    Transform _playerTransform;

    void Start()
    {
        _playerTransform = PlayerTransform.Value;
        _playerGridPos = GetPlayerGridPos();
        SpawnNewCells();
    }

    void Update()
    {
        Vector2Int newPos = GetPlayerGridPos();

        // if player hasn't changed grid position, do nothing
        if (newPos.Equals(_playerGridPos)) return;

        _playerGridPos = newPos;
        RemoveOldCells();
        SpawnNewCells();
    }

    Vector2Int GetPlayerGridPos() => Grid.GetGridPos(Vector3.zero, cellSize, _playerTransform.position);

    void SpawnNewCells()
    {
        foreach (Vector2Int pos in _spawnPattern)
        {
            var cellPos = new Vector2Int(_playerGridPos.x + pos.x, _playerGridPos.y + pos.y);
            if (_cells.ContainsKey(cellPos)) continue;

            Vector3 worldPos = Grid.GetWorldPos(Vector3.zero, cellSize, cellPos.x, cellPos.y, true);
            GameObject cell = PoolManager.Spawn(cellPrefab.GetRandom(), worldPos, Quaternion.identity, transform);
            _cells[cellPos] = cell;
        }
    }

    void RemoveOldCells()
    {
        // Mark cells for removal
        _removalSet.Clear();
        foreach (Vector2Int pos in _cells.Keys)
        {
            int diffX = Mathf.Abs(_playerGridPos.x - pos.x);
            int diffY = Mathf.Abs(_playerGridPos.y - pos.y);
            if (diffX >= 2 || diffY >= 2)
                _removalSet.Add(pos);
        }

        // Remove marked cells
        foreach (Vector2Int pos in _removalSet)
        {
            if (_cells.TryGetValue(pos, out GameObject cell) && cell != null)
                PoolManager.Despawn(cell);
            _cells.Remove(pos);
        }
    }
}
