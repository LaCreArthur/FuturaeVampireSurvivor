using UnityEngine;

public abstract class MoveBehavior : MonoBehaviour
{
    public float speed = 5f;
    public abstract void Move();
    public virtual void Initialize() {}
}
