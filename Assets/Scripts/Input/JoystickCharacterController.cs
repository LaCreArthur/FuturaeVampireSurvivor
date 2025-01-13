using UnityEngine;

public class JoystickCharacterController : MonoBehaviour
{
    const float DEAD_ZONE = 0.1f;

    [SerializeField] bool isJoystickDirectionInverted;
    [SerializeField] float joystickSensitivity = 10f;
    [SerializeField] Animator animator;
    [SerializeField] AnimationCurve accelerationCurve;

    Vector2 _joystickDirection; //todo: get this
    Vector3 _initialClickPosition;

    Quaternion _targetRotation;
    float _timer;
    float DirSign => isJoystickDirectionInverted ? -1 : 1;

    void Update()
    {
        if (_joystickDirection.magnitude > DEAD_ZONE)
        {
            Move();
        }
    }

    void Move() => transform.position +=
        new Vector3(DirSign * _joystickDirection.x, 0f, DirSign * _joystickDirection.y) *
        (joystickSensitivity * Time.deltaTime);
}
