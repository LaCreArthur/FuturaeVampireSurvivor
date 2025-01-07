using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LookAtDirection2D : MonoBehaviour
{
    Rigidbody2D _rb;

    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 5f; // How fast the rotation happens
    [SerializeField] float maxUpAngle = 30f;   // Max angle when moving upwards
    [SerializeField] float maxDownAngle = -60f; // Max angle when falling down

    // Cache Rigidbody2D reference
    void Start() => _rb = GetComponent<Rigidbody2D>();

    void Update() => LookInDirectionOfMovement();

    void LookInDirectionOfMovement()
    {
        // Calculate target angle based on velocity
        float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;

        // Clamp angle to prevent unnatural rotations
        float clampedAngle = Mathf.Clamp(angle, maxDownAngle, maxUpAngle);

        // Smoothly interpolate to the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, clampedAngle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
