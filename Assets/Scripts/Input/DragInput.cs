using UnityEngine;

public class DragInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] float speedToReachDestination = 10f;
    [SerializeField] float dragScale = 1f;

    Camera _mainCamera;
    Vector3 _initialObjectPosition;
    Vector3 _initialPointerWorldPosition;
    Vector2 _inputVector;
    float _zDistance;
    bool _isDragging;

    void Start()
    {
        _mainCamera = Camera.main;
        _initialObjectPosition = transform.position;
        ResetInput();
    }


    public Vector2 ReadInput()
    {
        if (Input.touchCount > 0)
        {
            HandleTouchInput(Input.GetTouch(0));
        }
        else
        {
            HandleMouseInput();
        }
        return _inputVector;
    }

    void ResetInput() => _inputVector = Vector2.zero;

    void HandleTouchInput(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                BeginDrag(touch.position);
                break;

            case TouchPhase.Moved:
                if (_isDragging)
                {
                    PerformDrag(touch.position);
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                EndDrag();
                break;
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            PerformDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    void BeginDrag(Vector3 screenPosition)
    {
        _isDragging = true;
        _zDistance = Mathf.Abs(transform.position.z - _mainCamera.transform.position.z);
        _initialObjectPosition = transform.position;
        _initialPointerWorldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _zDistance));
        _inputVector = Vector2.zero;
    }

    void PerformDrag(Vector3 screenPosition)
    {
        Vector3 currentPointerWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _zDistance));
        Vector3 delta = (currentPointerWorldPos - _initialPointerWorldPosition) * dragScale;
        Vector3 targetPosition = _initialObjectPosition + delta;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, speedToReachDestination * Time.deltaTime);
        Vector3 movement = smoothedPosition - transform.position;

        _inputVector = new Vector2(movement.x, movement.z);
    }

    void EndDrag()
    {
        _isDragging = false;
        _inputVector = Vector2.zero;
    }
}
