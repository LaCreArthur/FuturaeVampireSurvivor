using UnityEngine;

public class LookTowardDirection : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float minMovementThreshold = 0.01f;

    Vector3 _previousPosition;

    void Start() => _previousPosition = transform.position;

    void Update()
    {
        Vector3 movement = transform.position - _previousPosition;

        if (movement.sqrMagnitude > minMovementThreshold * minMovementThreshold)
        {
            // Calculate the target rotation based on movement direction
            float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

            // Smoothly rotate to face movement direction
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        _previousPosition = transform.position;
    }
}
