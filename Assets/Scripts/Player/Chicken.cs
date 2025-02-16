using System;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] int hp;

    public static event Action<int> ChickenCollected;

    void Awake() => GameStateManager.OnGameOver += Despawn;

    void OnDestroy() => GameStateManager.OnGameOver -= Despawn;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChickenCollected?.Invoke(hp);
            Despawn();
        }
    }

    void Despawn() => PoolManager.Despawn(gameObject);
}
