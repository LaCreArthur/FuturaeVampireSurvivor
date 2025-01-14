using UnityEngine;

public class DragInput : MonoBehaviour, IPlayerInput
{
    bool _isDragging;
    Vector2 _input;
    Camera _mainCam;

    Vector3 _initialPos;

    // Positions stored at the start of dragging
    Vector3 _objectStartPos; // Initial object position
    Vector3 _pointerStartWorldPos; // Initial pointer position in world coordinates
    float _zCoord; // Distance between camera and object

    void Start()
    {
        _mainCam = Camera.main;
        _initialPos = transform.position;
        GameStateManager.OnHome += ResetInput;
        ResetInput();
    }

    void Update()
    {
        // Detect input (Touch on mobile, Mouse on PC)
        if (Input.touchCount > 0)
        {
            HandleTouchInput(Input.GetTouch(0));
        }
        else
        {
            HandleMouseInput();
        }
    }

    void OnDestroy() => GameStateManager.OnHome -= ResetInput;

    public Vector2 ReadInput() => _input;

    void ResetInput() => _input = new Vector2(_initialPos.x, _initialPos.z);

    void HandleTouchInput(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Began:
                StartDragging(touch.position);
                break;

            case TouchPhase.Moved:
                if (_isDragging)
                {
                    UpdateDragging(touch.position);
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                StopDragging();
                break;
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            UpdateDragging(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    void StartDragging(Vector3 screenPosition)
    {
        _isDragging = true;

        // Calculate Z distance between camera and object
        _zCoord = transform.position.z - _mainCam.transform.position.z;

        // Store initial object position
        _objectStartPos = transform.position;

        // Convert pointer position to world coordinates
        _pointerStartWorldPos = _mainCam.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, _zCoord)
        );

        _input = new Vector2(_objectStartPos.x, _objectStartPos.z);
    }

    void UpdateDragging(Vector3 screenPosition)
    {
        // Convert current pointer position to world coordinates
        Vector3 pointerCurrentWorldPos = _mainCam.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, _zCoord)
        );

        // Calculate difference (delta) and apply scale factor
        Vector3 delta = pointerCurrentWorldPos - _pointerStartWorldPos;

        // Calculate new position by adding delta to initial position
        Vector3 newPosition = _objectStartPos + delta;

        _input = new Vector2(newPosition.x, newPosition.z);
    }

    void StopDragging() => _isDragging = false;
}
