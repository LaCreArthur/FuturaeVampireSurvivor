using UnityEngine;

public class AxeProjectileController : MonoBehaviour
{

    // Configuration
    [SerializeField] float baseGravity = 4f;
    [SerializeField] float maxHorizontalDeviation = 0.5f;
    [SerializeField] float rotationSpeed = 360f;

    // Movement parameters
    float _verticalSpeed;
    float _horizontalAmplitude;
    float _gravity;
    float _currentAirTime;
    Vector2 _initialDirection;

    void Update()
    {
        _currentAirTime += Time.deltaTime;

        // Simulated vertical physics
        _verticalSpeed -= _gravity * Time.deltaTime;
        float verticalMovement = _verticalSpeed * Time.deltaTime;

        // Horizontal oscillation with easing
        float horizontalFactor = Mathf.Sin(_currentAirTime * 5f) *
                                 Mathf.Lerp(maxHorizontalDeviation, 0, _currentAirTime / 2);
        Vector2 movement = _initialDirection * verticalMovement +
                           new Vector2(horizontalFactor * Time.deltaTime, 0);

        transform.Translate(movement, Space.World);
        ApplyRotation(movement);
    }

    public void Initialize(float speed, int damage, GameObject attacker)
    {
        _verticalSpeed = speed;
        _gravity = baseGravity * Random.Range(0.8f, 1.2f);
        _horizontalAmplitude = Random.Range(maxHorizontalDeviation * 0.5f, maxHorizontalDeviation);
        _initialDirection = transform.up;

        // Random initial angle offset
        transform.Rotate(0, 0, Random.Range(-15f, 15f));
    }

    void ApplyRotation(Vector2 movementDirection)
    {
        // Dynamic rotation based on movement
        float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.AngleAxis(targetAngle, Vector3.forward),
            rotationSpeed * Time.deltaTime
        );
    }
}
