using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //todo: this class purpose is unclear and should be refactored 

    //static ref to transform for quick reference in other scripts
    public static Transform PlayerTransform;
    Rigidbody _rb;

    void Awake() => PlayerTransform = transform;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GameStateManager.OnInGame += OnInGame;
        GameStateManager.OnGameOver += OnGameOver;
    }

    void OnDestroy()
    {
        GameStateManager.OnInGame -= OnInGame;
        GameStateManager.OnGameOver -= OnGameOver;
    }
    void OnGameOver()
    {
        _rb.isKinematic = false;
        _rb.linearVelocity = Vector3.zero;
    }

    void OnInGame() => _rb.isKinematic = true;
}
