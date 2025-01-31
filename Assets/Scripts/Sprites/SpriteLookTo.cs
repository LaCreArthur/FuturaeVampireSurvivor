using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class SpriteLookTo : MonoBehaviour
{
    [SerializeField] bool inverted;

    protected bool lookingLeft;

    SpriteRenderer _spriteRenderer;
    bool _previousLookingLeft;

    void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

    void Update()
    {
        SetLookingLeft();
        FlipIfNeeded();
    }

    protected abstract void SetLookingLeft();

    void FlipIfNeeded()
    {
        if (inverted) lookingLeft = !lookingLeft;
        if (lookingLeft != _previousLookingLeft)
        {
            _spriteRenderer.flipX = lookingLeft;
            _previousLookingLeft = lookingLeft;
        }
    }
}
