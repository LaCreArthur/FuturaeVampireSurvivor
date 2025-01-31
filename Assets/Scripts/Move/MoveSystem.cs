using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    MoveBehavior _moveBehavior;
    MoveBehavior MoveBehavior
    {
        get => _moveBehavior;
        set {
            _moveBehavior = value;
            _moveBehavior.Initialize();
        }
    }

    // move behavior can be null (no movement), so we need to check for null
    void Awake() => _moveBehavior = GetComponent<MoveBehavior>();
    void Start() => MoveBehavior?.Initialize();
    void Update() => MoveBehavior?.Move();
}
