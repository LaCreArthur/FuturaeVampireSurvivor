using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;
    [SerializeField] Vector3 bulletOffset;

    float _nextSpawnTime;
    float SpawnInterval => 1f / shootRate;

    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            Spawn();
            _nextSpawnTime = Time.time + SpawnInterval;
        }
    }

    void Spawn()
    {
        Vector3 position = transform.TransformPoint(bulletOffset);
        PoolManager.Spawn(bullet, position, transform.rotation, null);
    }
}
