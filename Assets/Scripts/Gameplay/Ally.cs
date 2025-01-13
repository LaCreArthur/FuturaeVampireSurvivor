using UnityEngine;

public class Ally : MonoBehaviour, ICollidable
{
    public CollisionType CollisionType => CollisionType.Ally;
}
