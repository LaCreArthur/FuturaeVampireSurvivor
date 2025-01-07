using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float jumpForce;

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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _rb.linearVelocity = Vector2.up * jumpForce;
    }

    void OnDestroy() => GameManager.OnGameRestart -= ResetPlayer;
    void OnCollisionEnter2D(Collision2D other) => GameManager.OnGameEnd?.Invoke();
    void OnTriggerEnter2D(Collider2D other) => ScoreManager.IncreaseScore();
    void ResetPlayer()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rb.linearVelocity = Vector2.zero;
    }
}
