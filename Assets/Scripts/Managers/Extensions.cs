using UnityEngine;

public static class Extensions
{
    public static void LookAt2D(this Transform t, Vector3 targetPos)
    {
        // Calculate the direction vector
        Vector2 direction = targetPos - t.position;

        // Calculate the angle and set the rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        t.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjust by -90 degrees to align with up direction
    }
}
