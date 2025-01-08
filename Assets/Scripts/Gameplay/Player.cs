using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float smoothTime = 0.1f;

    Vector3 _velocity = Vector3.zero;
    Vector3 _targetPosition;
    bool _isDragging;
    float _startMouseX;
    float _startPlayerX;

    Rigidbody2D _rb;
    Vector3 _startPosition;
    Quaternion _startRotation;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Start() => GameManager.OnGameRestart += ResetPlayer;
    void OnDestroy() => GameManager.OnGameRestart -= ResetPlayer;
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
            GameManager.OnGameEnd?.Invoke();
    }

    void ResetPlayer()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rb.linearVelocity = Vector2.zero;
    }
}
