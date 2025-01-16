using UnityEngine;

public class ImpulseTowardPlayerObjectAnimation : MonoBehaviour, IObjectAnimation
{
    [SerializeField] float magnitude;
    [SerializeField] Vector3 offset;

    Rigidbody _rb;
    Transform _player;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = PlayerController.PlayerTransform;
    }

    public void PlayAnimation()
    {
        Vector3 forceDirection = (_player.position + offset - transform.position).normalized * magnitude;
        _rb.AddForce(forceDirection, ForceMode.Impulse);
    }
}
