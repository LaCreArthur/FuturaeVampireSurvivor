using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookToDir : MonoBehaviour
{
    const float DEAD_ZONE = 0.001f;
    [SerializeField] bool inverted;

    SpriteRenderer _spriteRenderer;
    Vector3 _previousPosition;
    bool _lookingLeft;
    bool _previousLookingLeft;

    void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

    void Update()
    {
        _lookingLeft = transform.position.x - _previousPosition.x < -DEAD_ZONE;
        if (inverted) _lookingLeft = !_lookingLeft;
        if (_lookingLeft != _previousLookingLeft)
        {
            _spriteRenderer.flipX = _lookingLeft;
            _previousLookingLeft = _lookingLeft;
        }
        _previousPosition = transform.position;
    }
}
