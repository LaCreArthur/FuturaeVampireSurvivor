using System.Collections.Generic;
using UnityEngine;

public class ObjectAttractor : MonoBehaviour
{
    [SerializeField] float attractionForce = 10f;

    readonly List<Transform> _attractedObjects = new List<Transform>();
    CircleCollider2D _collider;

    void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        ModifierSystem.CharacterModifiersUpdated += OnModifiersUpdated;
    }

    void Update()
    {
        // Loop in reverse so we can remove items safely
        for (int i = _attractedObjects.Count - 1; i >= 0; i--)
        {
            Transform attractedObject = _attractedObjects[i];

            // If the object is inactive (despawned), remove it
            if (attractedObject.gameObject.activeSelf == false)
            {
                _attractedObjects.RemoveAt(i);
                continue;
            }

            // Move the object towards this attractor
            Vector2 direction = (transform.position - attractedObject.position).normalized;
            attractedObject.position += (Vector3)direction * (attractionForce * Time.deltaTime);
        }
    }

    void OnDestroy() => ModifierSystem.CharacterModifiersUpdated -= OnModifiersUpdated;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Match with layer "Attractables"
        if (other.gameObject.layer == LayerMask.NameToLayer("Attractables"))
        {
            // Add to the list if not already included
            if (!_attractedObjects.Contains(other.transform))
            {
                _attractedObjects.Add(other.transform);
            }
        }
    }

    void OnModifiersUpdated(Modifiers stats) => _collider.radius = stats.attractionRange / 10f;
}
