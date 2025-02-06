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
        _healthSystem.DamageTaken += DamageTaken;
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

    void DamageTaken()
    {
        if (_blinkCoroutine != null) StopCoroutine(_blinkCoroutine);
        _blinkCoroutine = StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < blinks; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
