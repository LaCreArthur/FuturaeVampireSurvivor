using UnityEngine;

public class PlayerReseter : MonoBehaviour
{
    Vector3 _startPosition;
    Quaternion _startRotation;

    void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Start() => GameStateManager.OnHome += ResetPlayer;
    void OnDestroy() => GameStateManager.OnHome -= ResetPlayer;

    void ResetPlayer()
    {
        Debug.Log("Resetting player position and rotation.");
        transform.position = _startPosition;
        transform.rotation = _startRotation;
    }
}
