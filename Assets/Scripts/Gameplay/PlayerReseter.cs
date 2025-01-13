using UnityEngine;

public class PlayerReseter : MonoBehaviour
{
    Rigidbody _rb;
    Vector3 _startPosition;
    Quaternion _startRotation;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Start() => GameManager.OnGameRestart += ResetPlayer;
    void OnDestroy() => GameManager.OnGameRestart -= ResetPlayer;

    void ResetPlayer()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rb.linearVelocity = Vector2.zero;
    }
}
