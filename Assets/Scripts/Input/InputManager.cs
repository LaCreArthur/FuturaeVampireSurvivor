using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputType InputType;

    [Header("Shared Joystick Settings")]
    [SerializeField] float joystickSensitivity = 1;

    [Header("Movement Constraints")]
    [Tooltip("Freeze movement on X axis if true.")]
    public bool freezeX;
    [Tooltip("Freeze movement on Z axis if true.")]
    public bool freezeZ;

    [SerializeField] InputType inputType;
    [SerializeField] UIJoystickInput uiJoystickInput;
    [SerializeField] DragInput dragInput;

    AxisJoystickInput _axisJoystickInput;
    IPlayerInput _playerInput;

    void Start()
    {
        _axisJoystickInput = new AxisJoystickInput(joystickSensitivity);
        uiJoystickInput.JoystickSensitivity = joystickSensitivity;
    }

    void Update()
    {
        if (GameStateManager.CurrentState != GameState.InGame) return;
        SelectInputType();
        Move();
    }

    void SelectInputType()
    {
        // in case the input type is changed during gameplay
        _playerInput = inputType switch
        {
            InputType.AxisJoystick => _axisJoystickInput,
            InputType.UIJoystick => uiJoystickInput,
            InputType.Drag => dragInput,
            _ => _playerInput,
        };
        InputType = inputType;
    }

    void Move()
    {
        Vector2 input = _playerInput.ReadInput();
        if (freezeX) input.x = 0;
        if (freezeZ) input.y = 0;
        transform.position += new Vector3(input.x, 0, input.y);
    }
}

public enum InputType
{
    AxisJoystick,
    UIJoystick,
    Drag,
}
