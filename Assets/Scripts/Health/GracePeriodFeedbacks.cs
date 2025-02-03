using System.Collections;
using UnityEngine;

public class GracePeriodFeedbacks : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    bool _isInGracePeriod;
    Coroutine _gracePeriodBlinkCoroutine;
    HealthSystem _healthSystem;

    void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        if (_healthSystem == null) _healthSystem = GetComponentInParent<HealthSystem>();
        if (_healthSystem == null)
        {
            Debug.LogWarning("No HealthSystem found in parent or self", this);
            enabled = false;
            return;
        }
        _healthSystem.OnGracePeriodStatusChanged += OnGracePeriodStatusChanged;
    }

    // player should have only one sprite renderer of the character
    void OnTransformChildrenChanged()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("No SpriteRenderer found in children or self", this);
            enabled = false;
        }
    }

    void OnGracePeriodStatusChanged(bool isInGracePeriod)
    {
        _isInGracePeriod = isInGracePeriod;
        if (_isInGracePeriod) _gracePeriodBlinkCoroutine = StartCoroutine(GracePeriodBlink());
        else
        {
            StopCoroutine(_gracePeriodBlinkCoroutine);
            spriteRenderer.enabled = true;
        }
    }

    IEnumerator GracePeriodBlink()
    {
        while (_isInGracePeriod)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
