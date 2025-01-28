using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //todo: this class purpose is unclear and should be refactored 
    //static ref to transform for quick reference in other scripts
    public static Transform PlayerTransform;
    Rigidbody2D _rb;

    void Awake() => PlayerTransform = transform;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameStateManager.OnPlaying += OnInGame;
        GameStateManager.OnGameOver += OnGameOver;
    }

    void OnDestroy()
    {
        GameStateManager.OnPlaying -= OnInGame;
        GameStateManager.OnGameOver -= OnGameOver;
    }
    void OnGameOver()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        _rb.linearVelocity = Vector3.zero;
    }

    void OnInGame() => _rb.bodyType = RigidbodyType2D.Kinematic;
}
