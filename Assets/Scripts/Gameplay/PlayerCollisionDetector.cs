using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            var poolable = other.gameObject.GetComponent<DespawnPoolableByDistance>();
            PoolManager.Despawn(poolable.prefab, other.gameObject);
        }
    }
}
