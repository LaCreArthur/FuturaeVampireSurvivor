using UnityEngine;

public class InputManager : MonoBehaviour
{
    public const float DEAD_ZONE = 0.001f;
    // static ref to share the Input value with other scripts
    public static Vector2 Input;

    [SerializeField] float moveSpeedFactor = 0.12f;
    [SerializeField] bool freezeX;
    [SerializeField] bool freezeZ;
    [SerializeField] bool normalized;

    Vector2 _direction;
    float _moveSpeed;

    void Awake()
    {
        GameStateManager.OnStateChange += OnStateChanged;
        ModifierSystem.CharacterModifiersUpdated += OnModifiersUpdated;
    }

    void Update() => Move();
    void OnDestroy()
    {
        GameStateManager.OnStateChange -= OnStateChanged;
        ModifierSystem.CharacterModifiersUpdated -= OnModifiersUpdated;
    }
    void OnModifiersUpdated(Modifiers stats) => _moveSpeed = stats.moveSpeed * moveSpeedFactor;
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

        _direction = Input * (_moveSpeed * Time.deltaTime);
        transform.position += new Vector3(_direction.x, _direction.y, 0);
    }
}
