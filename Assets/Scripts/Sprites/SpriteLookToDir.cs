using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookToDir : SpriteLookTo
{
    Vector3 _previousPosition;

    protected override void SetLookingLeft()
    {
        lookingLeft = transform.position.x < _previousPosition.x;
        _previousPosition = transform.position;
    }
}
