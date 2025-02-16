using UnityEngine;

public class BreakableSpotSpawner : MonoBehaviour, IPoolable
{
    [SerializeField] GameObject prefab;
    [SerializeField] BreakableSpot[] spots;

    void OnDisable()
    {
        foreach (BreakableSpot spot in spots)
        {
            if (spot.pickableSpawner != null && spot.pickableSpawner.IsSpawned)
            {
                PoolManager.Despawn(spot.pickableSpawner.gameObject);
                spot.pickableSpawner = null;
            }
        }
    }

    // on cell spawn, we spawn the breakables at the spots
    public void OnSpawn() => SpawnSpots();

    void SpawnSpots()
    {
        foreach (BreakableSpot spot in spots)
        {
            if (spot.pickableSpawner != null && spot.pickableSpawner.IsSpawned)
            {
                spot.pickableSpawner.transform.position = spot.transform.position;
            }
            else
            {
                spot.pickableSpawner = PoolManager.Spawn(prefab, spot.transform.position, Quaternion.identity).GetComponent<PickableSpawner>();
            }
        }
    }
}
