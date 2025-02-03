using System.Collections;
using UnityEngine;

public class DamageBlinkFeedbacks : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinks;

    Coroutine _blinkCoroutine;
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
        _healthSystem.OnDamage += OnDamage;
    }

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

    void OnDamage()
    {
        StopCoroutine(_blinkCoroutine);
        _blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < blinks; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
