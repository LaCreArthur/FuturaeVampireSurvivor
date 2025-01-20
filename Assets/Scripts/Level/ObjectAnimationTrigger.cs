using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimationTrigger : MonoBehaviour
{
    [SerializeField] List<PoolableRbSpot> spots;
    [SerializeField] float delay = 0.5f;

    public List<GameObject> _pooledObjects = new List<GameObject>();
    // the segment that this trigger belongs to
    Segment _segment;

    void Awake()
    {
        _segment = GetComponentInParent<Segment>();
        // because objects are spawned, we need to wait for the segment to finish spawning them to get the references
        _segment.SpotObjectsSpawned += OnSpotObjectsSpawned;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(AnimationCoroutine());
        }
    }
    void OnSpotObjectsSpawned()
    {
        _pooledObjects.Clear();
        for (int i = 0; i < spots.Count; i++)
        {
            _pooledObjects.Add(spots[i].activeInstance);
        }
    }

    IEnumerator AnimationCoroutine()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            GameObject o = _pooledObjects[i];
            // thanks to the IObjectAnimation interface, we can call PlayAnimation regardless of the animation type
            if (o.TryGetComponent(out IObjectAnimation objectAnimation))
            {
                objectAnimation.PlayAnimation();
            }
            yield return new WaitForSeconds(delay);
        }
    }
}
