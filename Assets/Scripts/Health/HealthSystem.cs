using System;
using DG.Tweening;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] [ReadOnly] protected int currentHealth;

    public event Action OnDamage;
    public event Action OnDeath;

    public virtual int CurrentHealth
    {
        get => currentHealth;
        protected set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public virtual int MaxHealth
    {
        get => maxHealth;
        protected set => maxHealth = value;
    }

    public virtual void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        OnDamage?.Invoke();
        if (CurrentHealth <= 0) OnDeath?.Invoke();
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
    }
}
