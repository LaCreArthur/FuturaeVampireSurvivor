using UnityEngine;

public class ImpulseForceObjectAnimation : MonoBehaviour, IObjectAnimation
{
    [SerializeField] Vector3 forceDirection;

    Rigidbody _rb;

    void Start() => _rb = GetComponent<Rigidbody>();

    public void PlayAnimation()
    {
        Debug.Log("Playing object animation.");
        _rb.AddForce(forceDirection, ForceMode.Impulse);
    }
}
