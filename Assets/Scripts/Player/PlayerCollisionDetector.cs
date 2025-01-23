using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    //todo: implement a proper collision detection system
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            // PoolManager.Despawn(other.gameObject);
        }
    }
}
