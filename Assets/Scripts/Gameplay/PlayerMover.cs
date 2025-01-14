using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    bool _isMoving;

    void Start()
    {
        GameStateManager.OnInGame += StartMoving;
        GameStateManager.OnGameOver += StopMoving;
    }

    void Update()
    {
        if (_isMoving)
        {
            transform.Translate(Vector3.right * (speed * Time.deltaTime));
        }
    }

    void OnDestroy()
    {
        GameStateManager.OnInGame -= StartMoving;
        GameStateManager.OnGameOver -= StopMoving;
    }

    void StartMoving() => _isMoving = true;
    void StopMoving() => _isMoving = false;
}
