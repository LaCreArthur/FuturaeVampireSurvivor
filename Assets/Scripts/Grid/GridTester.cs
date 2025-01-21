using UnityEngine;

public class GridTester : MonoBehaviour
{
    public int width, height;
    public float cellSize;
    public Vector3 originPos;
    public Orientation orientation;

    [ContextMenu("Test Grid")]
    void TestGrid()
    {
        var grid = new Grid<int>(width, height, cellSize, originPos, orientation);
        grid.DrawDebugLines(true);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var p = GameObject.CreatePrimitive(PrimitiveType.Cube);
                p.transform.position = grid.GetWorldPos(x, y);
            }
        }
    }
}
