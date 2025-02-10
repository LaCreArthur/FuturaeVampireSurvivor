using UnityEngine;

public static class Grid
{
    public static Vector3 GetWorldPos(Vector3 originPos, float cellSize, int x, int y, bool halfCellShift = false) =>
        new Vector3(x, y, 0) *
        cellSize + originPos +
        (halfCellShift ? new Vector3(1, 1, 0) * (cellSize / 2f) : Vector3.zero);

    public static Vector2Int GetGridPos(Vector3 originPos, float cellSize, Vector3 worldPos) =>
        new Vector2Int(
            Mathf.FloorToInt((worldPos - originPos).x / cellSize),
            Mathf.FloorToInt((worldPos - originPos).y / cellSize)
        );
}
