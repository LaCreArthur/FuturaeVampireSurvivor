using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out ICollidable collidable))
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
