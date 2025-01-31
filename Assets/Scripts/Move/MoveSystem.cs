using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] MoveBehavior moveBehavior;
    MoveBehavior MoveBehavior
    {
        get => moveBehavior;
        set {
            moveBehavior = value;
            moveBehavior.Initialize();
        }
    }

    // move behavior can be null (no movement), so we need to check for null
    void Start() => MoveBehavior?.Initialize();
    void Update() => MoveBehavior?.Move();
}
