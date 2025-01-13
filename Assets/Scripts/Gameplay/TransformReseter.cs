using UnityEngine;

public class TransformReseter : MonoBehaviour
{
    [SerializeField] GameState onState;

    Rigidbody _rb;
    Vector3 _startPosition;
    Quaternion _startRotation;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Start() => GameStateManager.OnStateChange += OnStateChanged;
    void OnDestroy() => GameStateManager.OnStateChange -= OnStateChanged;

    void OnStateChanged(GameState state)
    {
        if (state == onState)
        {
            ResetPlayer();
        }
    }

    void ResetPlayer()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rb.linearVelocity = Vector2.zero;
    }
}
