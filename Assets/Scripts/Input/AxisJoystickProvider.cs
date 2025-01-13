using UnityEngine;

public class AxisJoystickProvider : JoystickInputProvider
{
    [SerializeField] float sensitivity = 1;
    Vector2 _joystickInput;
    Vector2 InputVector
    {
        set {
            _joystickInput = value.magnitude > 1.0f ? value.normalized : value;
            OnJoystickInput(_joystickInput);
        }
    }

    void Start()
    {
#if !UNITY_EDITOR
        sensitivity *= 2;
#endif
    }
    void Update() => InputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * sensitivity;
}
