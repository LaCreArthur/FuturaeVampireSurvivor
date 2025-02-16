using System;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] int hp;

    public static event Action<int> ChickenCollected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChickenCollected?.Invoke(hp);
            PoolManager.Despawn(gameObject);
        }
    }
}
