using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float startSpeed = 5f;
    [SerializeField] float speedIncreasePerSecond = 0.05f;

    // static ref to transform for quick reference in other scripts
    public static Transform PlayerTransform;
    
    float _currentSpeed;
    bool _isMoving;
    Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        PlayerTransform = transform;
        GameStateManager.OnInGame += OnInGame;
        GameStateManager.OnGameOver += OnGameOver;
    }

    void Update()
    {
        if (!_isMoving) return;

        transform.position += Vector3.right * (_currentSpeed * Time.deltaTime);
        _currentSpeed += speedIncreasePerSecond * Time.deltaTime;
    }

    void OnDestroy()
    {
        GameStateManager.OnInGame -= OnInGame;
        GameStateManager.OnGameOver -= OnGameOver;
    }
    void OnGameOver()
    {
        _isMoving = false;
        _rb.isKinematic = false;
        _rb.linearVelocity = Vector3.zero;
    }

    void OnInGame()
    {
        _currentSpeed = startSpeed;
        _isMoving = true;
        _rb.isKinematic = true;
    }
}
