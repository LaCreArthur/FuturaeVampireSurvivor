using UnityEngine;

public class InputManager : MonoBehaviour
{
    public const float DEAD_ZONE = 0.001f;
    // static ref to share the Input value with other scripts
    public static Vector2 Input;

    [SerializeField] float moveSpeed = 1;
    [SerializeField] bool freezeX;
    [SerializeField] bool freezeZ;
    [SerializeField] bool normalized;

    Vector2 _direction;

    void Start() => GameStateManager.OnStateChange += OnStateChanged;
    void Update() => Move();
    void OnDestroy() => GameStateManager.OnStateChange -= OnStateChanged;
    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;

    static Vector2 ReadInput()
    {
        // automatically use the UI input if it's not zero
        Vector2 uiInput = UIJoystickInput.ReadInput();
        return uiInput.magnitude > 0 ? uiInput : AxisJoystickInput.ReadInput();
    }

    void Move()
    {
        Input = ReadInput();
        // dead zone check
        if (Input.sqrMagnitude < DEAD_ZONE) return;
        if (freezeX) Input.x = 0;
        if (freezeZ) Input.y = 0;
        if (normalized) Input.Normalize();

        _direction = Input * (moveSpeed * Time.deltaTime);
        transform.position += new Vector3(_direction.x, _direction.y, 0);
    }
}
