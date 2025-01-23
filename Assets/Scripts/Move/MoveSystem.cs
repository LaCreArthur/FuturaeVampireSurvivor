using UnityEngine;

public class MoveSystem : MonoBehaviour
{
    [SerializeField] MoveBehaviorSO moveBehavior;
    public MoveBehaviorSO MoveBehavior
    {
        get => moveBehavior;
        set {
            moveBehavior = value;
            moveBehavior.Initialize(gameObject);
        }
    }
    void Update() => MoveBehavior.Move(transform);
}
