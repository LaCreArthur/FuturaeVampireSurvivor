using UnityEngine;

public class DestructibleSphere : MonoBehaviour
{
    [SerializeField] Transform fragmentedSphere;
    [SerializeField] Transform realSphere;
    [SerializeField] float velocityMultiplier = .25f;
    [SerializeField] float forwardMultiplier = 1f;

    // For the fragments
    readonly Vector3[] _initialPositions = new Vector3[16];
    readonly Quaternion[] _initialRotations = new Quaternion[16];
    readonly Rigidbody[] _rigidbodies = new Rigidbody[16];

    Vector3 _previousPosition;
    Vector3 _currentVelocity;
    bool _isDestroyed;

    void Awake()
    {
        fragmentedSphere.gameObject.SetActive(false);
        for (int i = 0; i < fragmentedSphere.childCount; i++)
        {
            Transform fragment = fragmentedSphere.GetChild(i);
            _initialPositions[i] = fragment.localPosition;
            _initialRotations[i] = fragment.localRotation;
            _rigidbodies[i] = fragment.GetComponent<Rigidbody>();
        }
    }
    void Start()
    {
        GameStateManager.OnGameOver += DestroySphere;
        GameStateManager.OnHome += ResetSphere;
        _previousPosition = transform.position;
    }

    void Update()
    {
        // Calculate velocity based on position change over time
        _currentVelocity = (transform.position - _previousPosition) / Time.deltaTime;
        _previousPosition = transform.position;
    }

    void OnDestroy()
    {
        GameStateManager.OnGameOver -= DestroySphere;
        GameStateManager.OnHome -= ResetSphere;
    }

    void DestroySphere()
    {
        if (_isDestroyed) return;
        _isDestroyed = true;
        realSphere.gameObject.SetActive(false);
        fragmentedSphere.gameObject.SetActive(true);

        for (int i = 0; i < fragmentedSphere.childCount; i++)
        {
            Rigidbody fragmentRb = _rigidbodies[i];
            fragmentRb.AddForce(Vector3.right * forwardMultiplier + _currentVelocity.normalized * velocityMultiplier, ForceMode.Impulse);
        }

    }

    void ResetSphere()
    {
        _isDestroyed = false;
        realSphere.gameObject.SetActive(true);
        fragmentedSphere.gameObject.SetActive(false);

        for (int i = 0; i < fragmentedSphere.childCount; i++)
        {
            Transform fragment = fragmentedSphere.GetChild(i);
            fragment.localPosition = _initialPositions[i];
            fragment.localRotation = _initialRotations[i];
            _rigidbodies[i].linearVelocity = Vector3.zero;
        }
    }
}
