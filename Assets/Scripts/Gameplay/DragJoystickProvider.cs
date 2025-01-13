using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragJoystickProvider : JoystickInputProvider, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] Image outerCircleImage;
    [SerializeField] Image handleImage;
    [SerializeField] float maxDragDistanceInInches = 100f;
    [SerializeField] float sensitivity = 1;

    float _maxDragDistance;
    Vector2 _startPosition;
    Vector2 _joystickInput;
    Vector2 InputVector
    {
        set {
            // normalize the input vector if its magnitude is greater than 1
            _joystickInput = value.magnitude > 1.0f ? value.normalized : value;

            // Compute the desired position from the center of the outer circle
            Vector2 desiredPositionFromCenter = _joystickInput * _maxDragDistance;

            // Clamp the distance from the center to not exceed 100 units
            Vector2 clampedPositionFromCenter = Vector2.ClampMagnitude(desiredPositionFromCenter, 100f);

            // Set the handle's position relative to its starting position (center of the outer circle)
            handleImage.rectTransform.anchoredPosition = _startPosition + clampedPositionFromCenter;

            OnJoystickInput(_joystickInput);
        }
    }

    void OnEnable()
    {
        InputVector = Vector2.zero;
        SetImagesActive(false);
    }

    void Start()
    {
        _startPosition = handleImage.rectTransform.anchoredPosition;
        _maxDragDistance = maxDragDistanceInInches * Screen.dpi;
#if !UNITY_EDITOR
        sensitivity *= 2;
#endif
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPosition = eventData.position - (Vector2)outerCircleImage.rectTransform.position;
        InputVector = dragPosition / _maxDragDistance * sensitivity;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(outerCircleImage.rectTransform, eventData.position,
            eventData.pressEventCamera, out Vector2 localClickPosition);
        outerCircleImage.rectTransform.anchoredPosition += localClickPosition;
        _startPosition = handleImage.rectTransform.anchoredPosition;
        SetImagesActive(true);
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputVector = Vector2.zero;
        SetImagesActive(false);
    }

    void SetImagesActive(bool value)
    {
        outerCircleImage.enabled = value;
        handleImage.enabled = value;
    }
}
