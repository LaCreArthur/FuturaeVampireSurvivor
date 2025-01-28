using UnityEngine;

public class InputManager : MonoBehaviour
{
    // static ref to share the sensitivity for both UI and Axis input
    public static float JoystickSensitivity;

    [SerializeField] float joystickSensitivity = 1;
    [SerializeField] bool freezeX;
    [SerializeField] bool freezeZ;
    [SerializeField] Transform rotatingPart;


    void Awake() => SetSensitivity();
    void Start() => GameStateManager.OnStateChange += OnStateChanged;
    void Update() => Move();
    void OnDestroy() => GameStateManager.OnStateChange -= OnStateChanged;
    void SetSensitivity() => JoystickSensitivity = joystickSensitivity;
    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;

    static Vector2 ReadInput()
    {
        Vector2 uiInput = UIJoystickInput.ReadInput();
        return uiInput.magnitude > 0 ? uiInput : AxisJoystickInput.ReadInput();
    }

    void Move()
    {
        Vector2 input = ReadInput();
        // dead zone
        if (input.sqrMagnitude < 0.0001f) return;
        if (freezeX) input.x = 0;
        if (freezeZ) input.y = 0;
        Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0);
        rotatingPart.LookAt2D(targetPos);
        transform.position = targetPos;
    }
}
