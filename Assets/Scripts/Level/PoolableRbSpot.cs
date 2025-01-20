using UnityEngine;

/// <summary>
///     This class is used to mark a spot where a poolable rigidbody object can be spawned.
/// </summary>
public class PoolableRbSpot : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject activeInstance;
    public bool usePrefabScale;
}
