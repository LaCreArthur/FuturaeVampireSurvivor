using UnityEngine;

/// <summary>
///     Generic script that resets the transform of the GameObject it is attached to when a specific GameState is entered.
/// </summary>
public class ResetTransformOnState : MonoBehaviour
{
    [SerializeField] GameState state;
    [SerializeField] bool resetPosition = true;
    [SerializeField] bool resetRotation = true;
    [SerializeField] bool resetScale = true;

    Vector3 _startPosition;
    Quaternion _startRotation;
    Vector3 _startScale;

    void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _startScale = transform.localScale;
    }

    void Start() => GameStateManager.OnStateChange += ResetPlayer;
    void OnDestroy() => GameStateManager.OnStateChange -= ResetPlayer;

    void ResetPlayer(GameState newState)
    {
        if (newState != state) return;
        if (resetPosition) transform.position = _startPosition;
        if (resetRotation) transform.rotation = _startRotation;
        if (resetScale) transform.localScale = _startScale;
    }
}
