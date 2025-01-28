using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] List<WeaponBehavior> weaponBehaviors = new List<WeaponBehavior>();
    readonly Dictionary<WeaponBehavior, AttackTimer> _attackTimers = new Dictionary<WeaponBehavior, AttackTimer>();


    void Start() => GameStateManager.OnStateChange += OnStateChanged;

    void Update()
    {
        if (weaponBehaviors == null || weaponBehaviors.Count == 0) return;

        foreach (WeaponBehavior weaponBehavior in weaponBehaviors)
        {
            ExecuteAttackOfWeapon(weaponBehavior);
        }
    }
    void OnDestroy() => GameStateManager.OnStateChange -= OnStateChanged;
    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;
    void ExecuteAttackOfWeapon(WeaponBehavior weaponBehavior)
    {
        // if the weapon is not in the dictionary, add it
        if (!_attackTimers.TryGetValue(weaponBehavior, out AttackTimer attackTimer))
        {
            attackTimer = new AttackTimer(0, weaponBehavior.weaponData.cooldown);
            _attackTimers.Add(weaponBehavior, attackTimer);
        }

        // if the weapon is still on cooldown, decrease the cooldown time
        attackTimer.nextAttackTime -= Time.deltaTime;

        // if the weapon is ready to, execute an attack 
        if (attackTimer.nextAttackTime <= 0)
        {
            weaponBehavior.ExecuteAttack(gameObject);
            attackTimer.nextAttackTime = attackTimer.cooldown;
        }

        // update the dictionary
        _attackTimers[weaponBehavior] = attackTimer;
    }
}

[Serializable]
public struct AttackTimer
{
    public float nextAttackTime;
    public float cooldown;
    public AttackTimer(float nextAttackTime, float cooldown)
    {
        this.nextAttackTime = nextAttackTime;
        this.cooldown = cooldown;
    }
}
