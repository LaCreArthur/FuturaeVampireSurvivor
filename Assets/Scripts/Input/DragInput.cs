using UnityEngine;

public class DragInput : MonoBehaviour, IPlayerInput
{
    bool _isDragging;
    Vector2 _input;

    // Positions stored at the start of dragging
    Vector3 _objectStartPos; // Initial object position
    Vector3 _pointerStartWorldPos; // Initial pointer position in world coordinates
    float _zCoord; // Distance between camera and object

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

    public Vector2 ReadInput() => _input;

    /// <summary>
    ///     Handles touch inputs (mobile).
    /// </summary>
    /// <param name="touch">The current touch.</param>
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

    /// <summary>
    ///     Handles mouse inputs (PC).
    /// </summary>
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

    /// <summary>
    ///     Starts the dragging process.
    /// </summary>
    /// <param name="screenPosition">The screen position of the pointer (mouse or touch).</param>
    void StartDragging(Vector3 screenPosition)
    {
        _isDragging = true;

        // Calculate Z distance between camera and object
        _zCoord = transform.position.z - Camera.main.transform.position.z;

        // Store initial object position
        _objectStartPos = transform.position;

        // Convert pointer position to world coordinates
        _pointerStartWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, _zCoord)
        );

        _input = new Vector2(_objectStartPos.x, _objectStartPos.z);
    }

    /// <summary>
    ///     Updates object position during dragging.
    /// </summary>
    /// <param name="screenPosition">The current screen position of the pointer.</param>
    void UpdateDragging(Vector3 screenPosition)
    {
        // Convert current pointer position to world coordinates
        Vector3 pointerCurrentWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, _zCoord)
        );

        // Calculate difference (delta) and apply scale factor
        Vector3 delta = pointerCurrentWorldPos - _pointerStartWorldPos;

        // Calculate new position by adding delta to initial position
        Vector3 newPosition = _objectStartPos + delta;

        _input = new Vector2(newPosition.x, newPosition.z);
    }

    /// <summary>
    ///     Ends the dragging process.
    /// </summary>
    void StopDragging() => _isDragging = false;
}
