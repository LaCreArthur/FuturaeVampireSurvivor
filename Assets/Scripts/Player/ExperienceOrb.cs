using System;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] int exp;

    public static event Action<int> OnExpCollected;

    void Awake() => GameStateManager.OnGameOver += Despawn;

    void OnDestroy() => GameStateManager.OnGameOver -= Despawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnExpCollected?.Invoke(exp);
            Despawn();
        }
    }

    void Despawn() => PoolManager.Despawn(gameObject);
}
