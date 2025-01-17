using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoolableRBObject : MonoBehaviour, IPoolable
{
    const float DISTANCE_TO_DESPAWN = 10;

    [HideInInspector] public GameObject prefab;
    [SerializeField] int despawnCheckRefreshRate = 10;

    Rigidbody _rb;

    void Awake() => _rb = GetComponent<Rigidbody>();

    void Update()
    {
        // This only checks every few frames to save performance
        if (Time.frameCount % despawnCheckRefreshRate == 0)
        {
            if (transform.position.x < PlayerController.PlayerTransform.position.x - DISTANCE_TO_DESPAWN)
            {
                PoolManager.Despawn(prefab, gameObject);
            }
        }
    }

    public void OnSpawn()
    {
        _rb.linearVelocity = _rb.angularVelocity = Vector3.zero;
        _rb.Sleep();
    }

    public void OnDespawn() {}
}
