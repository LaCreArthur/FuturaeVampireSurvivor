using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookTowardInput : SpriteLookTo
{
    protected override void SetLookingLeft()
    {
        if (Mathf.Approximately(0, InputManager.Input.x)) return;
        lookingLeft = InputManager.Input.x < -InputManager.DEAD_ZONE;
    }
}
