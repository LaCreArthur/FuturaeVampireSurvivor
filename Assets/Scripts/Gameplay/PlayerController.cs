using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float startSpeed = 5f;
    [SerializeField] float speedIncreasePerSecond = 0.05f;

    float _currentSpeed;
    bool _isMoving;


    void Start()
    {
        GameStateManager.OnInGame += Restart;
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
        GameStateManager.OnInGame -= Restart;
        GameStateManager.OnGameOver -= OnGameOver;
    }
    void OnGameOver() => _isMoving = false;

    void Restart()
    {
        _currentSpeed = startSpeed;
        _isMoving = true;
    }
}
