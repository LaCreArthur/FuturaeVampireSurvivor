using UnityEngine;

public static class AxisJoystickInput
{
    public static Vector2 ReadInput() => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
}
