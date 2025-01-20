using UnityEngine;

public class ImpulseForceVectorObjectAnimation : MonoBehaviour, IObjectAnimation
{
    [SerializeField] Vector3 forceDirection;

    Rigidbody _rb;

    void Start() => _rb = GetComponent<Rigidbody>();
    public void PlayAnimation() => _rb.AddForce(forceDirection, ForceMode.Impulse);
}
