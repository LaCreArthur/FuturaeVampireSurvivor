using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookTowardInput : SpriteLookTo
{
    protected override void SetLookingLeft() => lookingLeft = InputManager.Input.x < InputManager.DEAD_ZONE;
}
