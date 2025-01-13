using System.Collections;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [Header("Joystick Settings")]
    [SerializeField] bool useAxis;
    [SerializeField] float movementSpeed = 10f;

    JoystickInputProvider _joystickInputProvider;

    bool _previousUseAxis;
    bool _isMoving;
    Vector3 _targetPos;
    Coroutine _co;

    void Update()
    {
        if (!_isMoving) return;

        Vector3 initialPosition = transform.position;

        Vector3 newPosition = Vector3.Lerp(initialPosition, _targetPos, movementSpeed * Time.deltaTime);
        transform.position = newPosition;

        if (newPosition == _targetPos) _isMoving = false;
    }



    public void OnEvent()
    {
        _isMoving = true;
        _targetPos = Vector3.zero;
        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (transform.position != _targetPos)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, _targetPos, movementSpeed * Time.deltaTime);
            transform.position = newPosition;
            yield return new WaitForEndOfFrame();
        }
    }
}
