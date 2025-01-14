using UnityEngine;

public class Enemy : MonoBehaviour, ICollidable
{
    public CollisionType CollisionType => CollisionType.Enemy;
}
