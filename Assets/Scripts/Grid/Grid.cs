using System;
using UnityEngine;

public class Grid
{
    readonly float _cellSize;
    readonly Orientation _orientation;
    readonly Vector3 _originPos;
    protected readonly int height;
    protected readonly int width;

    public Grid(int width, int height, float cellSize, Vector3 originPos, Orientation orientation)
    {
        this.width = width;
        this.height = height;
        _cellSize = cellSize;
        _originPos = originPos;
        _orientation = orientation;
    }

    public void DrawDebugLines(bool halfCellShift = false)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Debug.DrawLine(GetWorldPos(x, y, halfCellShift), GetWorldPos(x, y + 1, halfCellShift), Color.white, 100f);
                Debug.DrawLine(GetWorldPos(x, y, halfCellShift), GetWorldPos(x + 1, y, halfCellShift), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPos(0, height, halfCellShift), GetWorldPos(width, height, halfCellShift), Color.white, 100f);
        Debug.DrawLine(GetWorldPos(width, 0, halfCellShift), GetWorldPos(width, height, halfCellShift), Color.white, 100f);
    }

    public Vector3 GetWorldPos(int x, int y, bool halfCellShift = false) =>
        GetWorldPos(_orientation, _originPos, _cellSize, x, y, halfCellShift);

    public static Vector3 GetWorldPos(Orientation orientation, Vector3 originPos, float cellSize, int x, int y, bool halfCellShift = false)
    {
        float halfCell = cellSize / 2f;
        return orientation switch
        {
            Orientation.XY => new Vector3(x, y, 0) * cellSize +
                              originPos +
                              (halfCellShift ? new Vector3(1, 1, 0) * halfCell : Vector3.zero),
            Orientation.YZ => new Vector3(0, x, y) * cellSize +
                              originPos +
                              (halfCellShift ? new Vector3(0, 1, 1) * halfCell : Vector3.zero),
            Orientation.XZ => new Vector3(x, 0, y) * cellSize +
                              originPos +
                              (halfCellShift ? new Vector3(1, 0, 1) * halfCell : Vector3.zero),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public void GetXY(Vector3 worldPos, out int x, out int y) => GetXY(_orientation, _originPos, _cellSize, worldPos, out x, out y);

    public Vector2Int GetXY(Vector3 worldPos)
    {
        GetXY(_orientation, _originPos, _cellSize, worldPos, out int x, out int y);
        return new Vector2Int(x, y);
    }

    public static Vector2Int GetXY(Orientation orientation, Vector3 originPos, float cellSize, Vector3 worldPos)
    {
        GetXY(orientation, originPos, cellSize, worldPos, out int x, out int y);
        return new Vector2Int(x, y);
    }

    public static void GetXY(Orientation orientation, Vector3 originPos, float cellSize, Vector3 worldPos, out int x, out int y)
    {
        switch (orientation)
        {
            case Orientation.XY:
                x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
                y = Mathf.FloorToInt((worldPos - originPos).y / cellSize);
                break;
            case Orientation.YZ:
                x = Mathf.FloorToInt((worldPos - originPos).y / cellSize);
                y = Mathf.FloorToInt((worldPos - originPos).z / cellSize);
                break;
            case Orientation.XZ:
                x = Mathf.FloorToInt((worldPos - originPos).x / cellSize);
                y = Mathf.FloorToInt((worldPos - originPos).z / cellSize);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool IsValidXY(int x, int y) => x >= 0 && y >= 0 && x < width && y < height;
}

public class Grid<T> : Grid
{
    readonly T[,] _gridArray;

    public Grid(int width, int height, float cellSize, Vector3 originPos, Orientation orientation) : base(width, height, cellSize, originPos, orientation)
    {
        _gridArray = new T[width, height];
    }

    public void SetValue(int x, int y, T value)
    {
        if (IsValidXY(x, y))
            _gridArray[x, y] = value;
        else
            Debug.LogWarning($"Set value to invalid position x:{x} y:{y}");
    }

    public void SetValue(Vector3 worldPos, T value)
    {
        GetXY(worldPos, out int x, out int y);
        SetValue(x, y, value);
    }

    public bool GetValue(int x, int y, out T value)
    {
        if (IsValidXY(x, y))
        {
            value = _gridArray[x, y];
            return true;
        }

        Debug.LogWarning($"Tried to get a value from invalid position x:{x} y:{y}");
        value = default(T);
        return false;
    }

    public bool GetValue(Vector3 worldPos, out T value)
    {
        GetXY(worldPos, out int x, out int y);
        return GetValue(x, y, out value);
    }
}

public enum Orientation { XY, YZ, XZ }
