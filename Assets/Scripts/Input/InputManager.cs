using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputType InputType;
    [SerializeField] float joystickSensitivity = 1;
    [SerializeField] float dragScale = 1;

    [Header("Movement Constraints")]
    [Tooltip("Freeze movement on X axis if true.")]
    public bool freezeX;
    [Tooltip("Freeze movement on Z axis if true.")]
    public bool freezeZ;

    [SerializeField] InputType inputType;
    [SerializeField] UIJoystickInput uiJoystickInput;
    [SerializeField] DragInput dragInput;

    IPlayerInput _playerInput;
    void Update()
    {
        switch (inputType)
        {
            case InputType.AxisJoystick:
                _playerInput = new AxisJoystickInput();
                break;
            case InputType.UIJoystick:
                _playerInput = uiJoystickInput;
                break;
            case InputType.Drag:
                _playerInput = dragInput;
                break;
        }
        InputType = inputType;
        Vector2 input = _playerInput.ReadInput();

        switch (inputType)
        {
            case InputType.AxisJoystick:
            case InputType.UIJoystick:
                JoystickMove(input);
                break;
            case InputType.Drag:
                DragMove(input);
                break;
        }
    }

    void JoystickMove(Vector2 input)
    {
        if (freezeX) input.x = 0;
        if (freezeZ) input.y = 0;
        transform.position +=
            new Vector3(input.x, 0f, input.y) * (joystickSensitivity * Time.deltaTime);
    }

    void DragMove(Vector2 input)
    {
        if (freezeX) input.x = transform.position.x;
        if (freezeZ) input.y = transform.position.z;
        transform.position = new Vector3(input.x, 0f, input.y) * dragScale;
    }
}

public enum InputType
{
    AxisJoystick,
    UIJoystick,
    Drag,
}
