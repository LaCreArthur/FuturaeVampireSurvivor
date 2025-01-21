using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIJoystickInput : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] Image outerCircleImage;
    [SerializeField] Image handleImage;
    [SerializeField] float maxDragDistanceInInches = 100f;

    float _maxDragDistance;
    Vector2 _startPosition;
    static Vector2 s_input;

    static float JoystickSensitivity => InputManager.JoystickSensitivity;

    void OnEnable()
    {
        UpdateInput(Vector2.zero);
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
        // Calculate drag offset from the outer circle’s position
        Vector2 dragPosition = eventData.position - (Vector2)outerCircleImage.rectTransform.position;
        UpdateInput(dragPosition / _maxDragDistance);
    }

    public static Vector2 ReadInput() => s_input * (JoystickSensitivity * Time.deltaTime);

    public void OnPointerDown(PointerEventData eventData)
    {
        // Move the “outerCircleImage” to where the click happened
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            outerCircleImage.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localClickPos);

        outerCircleImage.rectTransform.anchoredPosition += localClickPos;

        // Reset the handle position reference
        _startPosition = handleImage.rectTransform.anchoredPosition;

        // Show the joystick UI
        SetImagesActive(true);

        // Immediately call OnDrag to set the initial vector
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset input and hide the joystick
        UpdateInput(Vector2.zero);
        SetImagesActive(false);
    }

    void UpdateInput(Vector2 value)
    {
        // Normalize if magnitude > 1
        s_input = value.magnitude > 1f ? value.normalized : value;

        // Scale and clamp the handle’s position relative to the center
        Vector2 desiredPosition = s_input * _maxDragDistance;
        Vector2 clampedPosition = Vector2.ClampMagnitude(desiredPosition, 100f);

        // Update the handle’s anchored position
        handleImage.rectTransform.anchoredPosition = _startPosition + clampedPosition;
    }

    void SetImagesActive(bool value)
    {
        outerCircleImage.enabled = value;
        handleImage.enabled = value;
    }
}
