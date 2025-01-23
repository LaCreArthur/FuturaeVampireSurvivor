using UnityEngine;

[RequireComponent(typeof(MonoBehaviour))]
public class AttackSystem : MonoBehaviour
{
    [SerializeField] AttackBehaviorSO attackBehavior;
    float _cooldownTimer;
    float _nextAttackTime;
    public AttackBehaviorSO AttackBehavior
    {
        get => attackBehavior;
        set {
            attackBehavior = value;
            _cooldownTimer = AttackBehavior.cooldown;
        }
    }

    void Start()
    {
        if (AttackBehavior != null) _cooldownTimer = AttackBehavior.cooldown;
        _nextAttackTime = _cooldownTimer;
    }

    void Update()
    {
        _nextAttackTime -= Time.deltaTime;
        if (_nextAttackTime <= 0)
        {
            PerformAttack();
            _nextAttackTime = _cooldownTimer;
        }
    }

    void PerformAttack() => AttackBehavior.ExecuteAttack(gameObject);
}
