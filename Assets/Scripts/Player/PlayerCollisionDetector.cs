using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            PoolManager.Despawn(other.gameObject);
        }
    }
}
