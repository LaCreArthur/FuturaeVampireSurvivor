using System.Collections;
using UnityEngine;

public class GracePeriodFeedbacks : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    bool _isInGracePeriod;
    Coroutine _gracePeriodBlinkCoroutine;

    void Start()
    {
        var healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnGracePeriodStatusChanged += OnGracePeriodStatusChanged;
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
