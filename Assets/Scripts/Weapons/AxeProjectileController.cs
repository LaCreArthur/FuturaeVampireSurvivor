using UnityEngine;

public class AxeProjectileController : MonoBehaviour, IPoolable
{
    // Serialized parameters for easy tuning
    [Header("Movement Settings")]
    [SerializeField] float baseGravity = 4f;
    [SerializeField] float maxHorizontalAmplitude = 0.5f;
    [SerializeField] float rotationSpeed = 360f;

    // Runtime variables
    int _damage;
    int _pierce;
    int _hitCount;
    float _verticalSpeed;
    float _gravity;
    float _currentAirTime;
    Vector2 _initialDirection;
    GameObject _attacker;

    void Update()
    {
        _currentAirTime += Time.deltaTime;

        // Vertical movement with simulated gravity
        _verticalSpeed -= _gravity * Time.deltaTime;
        float verticalMovement = _verticalSpeed * Time.deltaTime;

        // Horizontal oscillation with decaying amplitude
        float horizontalFactor = Mathf.Sin(_currentAirTime * 5f) *
                                 Mathf.Lerp(maxHorizontalAmplitude, 0, _currentAirTime / 2);

        transform.Translate(
            _initialDirection * verticalMovement +
            new Vector2(horizontalFactor * Time.deltaTime, 0),
            Space.World
        );

        ApplyRotation();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_attacker.tag)) return; // Ignore collisions with the attacker
        // using .attachedRigidbody because the collider can be on a child object of the target
        var otherHealth = other.attachedRigidbody?.GetComponent<HealthSystem>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(_damage); // Deal damage to the target
            _hitCount++;
            if (_hitCount >= _pierce)
            {
                PoolManager.Despawn(gameObject); // Despawn the projectile
            }
        }
    }

    public void OnSpawn() => _hitCount = 0;

    public void Initialize(float speed, int damage, int pierce, GameObject attacker)
    {
        _verticalSpeed = speed;
        _damage = damage;
        _pierce = pierce;
        _gravity = baseGravity * Random.Range(0.8f, 1.2f);
        _initialDirection = transform.up + transform.right * Random.Range(-0.2f, 0.2f);
        _attacker = attacker;

        // Random initial angle variation
        transform.Rotate(0, 0, Random.Range(-15f, 15f));
    }

    void ApplyRotation()
    {
        // Smooth rotation towards movement direction
        float currentRotation = transform.eulerAngles.z;
        float direction = _initialDirection.x < 0 ? 1 : -1;
        float targetRotation = currentRotation + rotationSpeed * Time.deltaTime * direction;
        transform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }
}
