using UnityEngine;

public class DespawnPoolableByDistance : MonoBehaviour
{
    const float DESPAWN_DISTANCE = 25;
    const int DESPAWN_CHECK_RATE = 60;
    // to avoid calling the more expensive Magnitude, and calling SqrMagnitude instead
    const float DESPAWN_DISTANCE_SQUARE = DESPAWN_DISTANCE * DESPAWN_DISTANCE;

    //todo: should not be it responsibility
    public GameObject prefab;

    void Start() => GameStateManager.OnHome += Despawn;

    void Update()
    {
        // This only checks every few frames to save performance
        if (Time.frameCount % DESPAWN_CHECK_RATE != 0) return;

        if (Vector3.SqrMagnitude(transform.position - PlayerController.PlayerTransform.position) > DESPAWN_DISTANCE_SQUARE)
        {
            Despawn();
        }
    }
    void OnDestroy() => GameStateManager.OnHome -= Despawn;

    void Despawn()
    {
        // Check if the object is enabled before despawning it (to avoid despawning it twice)
        if (gameObject.activeSelf) PoolManager.Despawn(prefab, gameObject);
    }
}
