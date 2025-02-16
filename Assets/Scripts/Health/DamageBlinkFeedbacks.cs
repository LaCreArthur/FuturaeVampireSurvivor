using System.Collections;
using UnityEngine;

public class DamageBlinkFeedbacks : MonoBehaviour, IPoolable
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float blinks;

    Coroutine _blinkCoroutine;
    HealthSystem _healthSystem;
    float _previousHp;

    void Start()
    {
        if (!TryGetComponent(out _healthSystem))
        {
            _healthSystem = GetComponentInParent<HealthSystem>();
            if (_healthSystem == null)
            {
                Debug.LogWarning("No HealthSystem found in parent or self", this);
                Destroy(this);
                return;
            }
        }

        _healthSystem.HpChanged += OnHpChanged;
        _previousHp = _healthSystem.CurrentHp;
    }

    void OnDestroy()
    {
        if (_healthSystem != null)
        {
            _healthSystem.HpChanged -= OnHpChanged;
        }
    }

    public void OnSpawn()
    {
        _previousHp = _healthSystem?.CurrentHp ?? 0;
        spriteRenderer.color = Color.white;
    }

    void OnHpChanged(float f)
    {
        if (_previousHp > f)
        {
            Blink();
        }
        _previousHp = f;
    }

    void Blink()
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
