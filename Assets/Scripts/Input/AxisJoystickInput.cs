using UnityEngine;

public class AxisJoystickInput
{
    static float JoystickSensitivity => InputManager.JoystickSensitivity;
    public static Vector2 ReadInput() => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * (JoystickSensitivity * Time.deltaTime);
}
