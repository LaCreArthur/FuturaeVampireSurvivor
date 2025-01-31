using UnityEngine;

[RequireComponent(typeof(Animator))] [RequireComponent(typeof(SpriteRenderer))]
public class CharacterAnimator : MonoBehaviour
{
    static readonly int Walking = Animator.StringToHash("walking");

    SpriteRenderer _spriteRenderer;
    Animator _animator;

    bool _walking;
    bool _previousWalking;

    bool _lookingLeft;
    bool _previousLookingLeft;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _walking = InputManager.Input.sqrMagnitude > InputManager.DEAD_ZONE;
        if (_walking != _previousWalking)
        {
            _animator.SetBool(Walking, _walking);
            _previousWalking = _walking;
        }

        if (!_walking) return;

        _lookingLeft = InputManager.Input.x < InputManager.DEAD_ZONE;
        if (_lookingLeft != _previousLookingLeft)
        {
            _spriteRenderer.flipX = _lookingLeft;
            _previousLookingLeft = _lookingLeft;
        }
    }
}
