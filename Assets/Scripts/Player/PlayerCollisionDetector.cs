using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    //todo: implement a proper collision detection system
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            // PoolManager.Despawn(other.gameObject);
        }
    }
}
