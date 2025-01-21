using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] float joystickSensitivity = 1;
    public bool freezeX;
    public bool freezeZ;

    public static float JoystickSensitivity;
    float _previousJoystickSensitivity;
    
    void Start() => OnValidate();

    void OnValidate()
    {
        if (!Mathf.Approximately(joystickSensitivity, _previousJoystickSensitivity))
        {
            JoystickSensitivity = joystickSensitivity;
            _previousJoystickSensitivity = joystickSensitivity;
        }
    }

    void Update()
    {
        if (GameStateManager.CurrentState != GameState.InGame) return;
        ReadInput();
        Move();
    }

    static Vector2 ReadInput()
    {
        Vector2 uiInput = UIJoystickInput.ReadInput();
        return uiInput.magnitude > 0 ? uiInput : AxisJoystickInput.ReadInput();
    }

    void Move()
    {
        Vector2 input = ReadInput();
        if (freezeX) input.x = 0;
        if (freezeZ) input.y = 0;
        transform.position += new Vector3(input.x, 0, input.y);
    }
}
