using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            // Debug.Log("Enemy collision detected!");
            GameStateManager.SetState(GameState.GameOver);
        }
        // else if (other.transform.CompareTag("Ally"))
        // Debug.Log("Ally collision detected!");
        // else
        // Debug.Log("Unknown collision detected!");
    }
}
