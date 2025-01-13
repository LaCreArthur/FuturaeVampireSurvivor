using UnityEngine;

public class AxisJoystickInput : IPlayerInput
{
    public Vector2 ReadInput() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
}
