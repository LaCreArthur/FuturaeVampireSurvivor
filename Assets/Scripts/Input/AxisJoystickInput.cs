using UnityEngine;

public class AxisJoystickInput : IPlayerInput
{
    readonly float _joystickSensitivity;
    public AxisJoystickInput(float joystickSensitivity)
    {
        _joystickSensitivity = joystickSensitivity;
    }
    public Vector2 ReadInput() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * (_joystickSensitivity * Time.deltaTime);
}
