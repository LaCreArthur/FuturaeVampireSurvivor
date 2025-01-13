using UnityEngine;

public class DragController : MonoBehaviour
{
    [Header("Drag Parameters")]
    [Tooltip("Movement multiplication factor.\n1 = identical movement,\n>1 = amplified movement,\n<1 = reduced movement.")]
    public float dragScale = 1f;

    [Header("Movement Constraints")]
    [Tooltip("Freeze movement on X axis if true.")]
    public bool freezeX;

    [Tooltip("Freeze movement on Z axis if true.")]
    public bool freezeZ;

    bool isDragging;

    // Positions stored at the start of dragging
    Vector3 objectStartPos; // Initial object position
    Vector3 pointerStartWorldPos; // Initial pointer position in world coordinates
    float zCoord; // Distance between camera and object

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
                if (isDragging)
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
        else if (Input.GetMouseButton(0) && isDragging)
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
        isDragging = true;

        // Calculate Z distance between camera and object
        zCoord = transform.position.z - Camera.main.transform.position.z;

        // Store initial object position
        objectStartPos = transform.position;

        // Convert pointer position to world coordinates
        pointerStartWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, zCoord)
        );
    }

    /// <summary>
    ///     Updates object position during dragging.
    /// </summary>
    /// <param name="screenPosition">The current screen position of the pointer.</param>
    void UpdateDragging(Vector3 screenPosition)
    {
        // Convert current pointer position to world coordinates
        Vector3 pointerCurrentWorldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, zCoord)
        );

        // Calculate difference (delta) and apply scale factor
        Vector3 delta = (pointerCurrentWorldPos - pointerStartWorldPos) * dragScale;

        // Calculate new position by adding delta to initial position
        Vector3 newPosition = objectStartPos + delta;

        // Apply movement constraints
        if (freezeX)
        {
            newPosition.x = objectStartPos.x;
        }

        if (freezeZ)
        {
            newPosition.z = objectStartPos.z;
        }

        // Keep Y position unchanged to lock Y movement (already handled by Rigidbody constraints if needed)
        newPosition.y = objectStartPos.y;

        // Apply final position
        transform.position = newPosition;
    }

    /// <summary>
    ///     Ends the dragging process.
    /// </summary>
    void StopDragging() => isDragging = false;
}
