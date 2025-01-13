using System;
using UnityEngine;

public abstract class JoystickInputProvider : MonoBehaviour
{
    [SerializeField] bool freezeX, freezeY;
    public event Action<Vector2> JoystickInput;
    protected void OnJoystickInput(Vector2 input) => JoystickInput?.Invoke(input);
}
