using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
            GameManager.OnGameEnd?.Invoke();
    }
}
