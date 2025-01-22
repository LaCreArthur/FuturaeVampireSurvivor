using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static float JoystickSensitivity;
    [SerializeField] float joystickSensitivity = 1;
    [SerializeField] bool freezeX;
    [SerializeField] bool freezeZ;
    float _previousJoystickSensitivity;

    void Start() => OnValidate();

    void Update()
    {
        if (GameStateManager.CurrentState != GameState.InGame) return;
        Move();
    }

    void OnValidate()
    {
        if (!Mathf.Approximately(joystickSensitivity, _previousJoystickSensitivity))
        {
            JoystickSensitivity = joystickSensitivity;
            _previousJoystickSensitivity = joystickSensitivity;
        }
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
