using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoolableRBObject : MonoBehaviour, IPoolable
{
    const float DISTANCE_TO_DESPAWN = 10;
    public GameObject prefab;
    [SerializeField] int despawnCheckRefreshRate = 10;

    Rigidbody _rb;

    void Awake() => _rb = GetComponent<Rigidbody>();

    void Start() => GameStateManager.OnHome += Despawn;

    void Update()
    {
        // This only checks every few frames to save performance
        if (Time.frameCount % despawnCheckRefreshRate == 0)
        {
            if (transform.position.x < PlayerController.PlayerTransform.position.x - DISTANCE_TO_DESPAWN)
            {
                Despawn();
            }
        }
    }
    void OnDestroy() => GameStateManager.OnHome -= Despawn;

    public void OnSpawn()
    {
        _rb.linearVelocity = _rb.angularVelocity = Vector3.zero;
        _rb.Sleep();
    }

    public void OnDespawn() {}

    void Despawn()
    {
        // Check if the object is enabled before despawning it (to avoid despawning it twice)
        if (gameObject.activeSelf) PoolManager.Despawn(prefab, gameObject);
    }
}
