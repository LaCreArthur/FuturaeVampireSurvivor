using System;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] int exp;

    public static event Action<int> OnExpCollected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnExpCollected?.Invoke(exp);
            PoolManager.Despawn(gameObject);
        }
    }
}
