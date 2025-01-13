using System;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<ICollidable>(out ICollidable collidable))
        {
            switch (collidable.CollisionType)
            {
                case CollisionType.Enemy:
                    Debug.Log("Enemy collision detected!");
                    GameStateManager.SetState(GameState.GameOver);
                    break;
                case CollisionType.Ally:
                    Debug.Log("Ally collision detected!");
                    break;
                default:
                    Debug.Log("Unknown collision detected!");
                    break;
            }
        }
    }
}

public enum CollisionType
{
    Enemy,
    Ally,
}

public class Enemy : MonoBehaviour, ICollidable
{
    public CollisionType CollisionType => CollisionType.Enemy;
}

public class Ally : MonoBehaviour, ICollidable
{
    public CollisionType CollisionType => CollisionType.Ally;
}

public interface ICollidable
{
    public CollisionType CollisionType { get; }
}
