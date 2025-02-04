using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] GameObject attacker;

    // Tracks how long until each weapon can attack again
    readonly Dictionary<WeaponBehavior, AttackTimer> _attackTimers = new Dictionary<WeaponBehavior, AttackTimer>();

    // Tracks all active weapon behaviors on child objects
    readonly List<WeaponBehavior> _weaponBehaviors = new List<WeaponBehavior>();

    public static event Action<WeaponBehavior> OnWeaponAdded;
    public static event Action<WeaponBehavior> OnWeaponRemoved;

    void Start()
    {
        GameStateManager.OnStateChange += OnStateChanged;
        // Ensure the list is populated if children exist at start.
        RefreshWeaponBehaviors();
    }

    void Update()
    {
        // Exits if no weapons
        if (_weaponBehaviors.Count == 0) return;

        // Iterate through each weapon and process attacks
        for (int i = 0; i < _weaponBehaviors.Count; i++)
        {
            WeaponBehavior weaponBehavior = _weaponBehaviors[i];

            // Skip inactive weapons
            if (!weaponBehavior.gameObject.activeSelf) continue;

            ExecuteAttackOfWeapon(weaponBehavior);
        }
    }

    void OnDestroy() => GameStateManager.OnStateChange -= OnStateChanged;

    void OnTransformChildrenChanged() => RefreshWeaponBehaviors();

    void RefreshWeaponBehaviors()
    {
        // Collect all relevant child WeaponBehaviors
        var currentChildren = new List<WeaponBehavior>();
        foreach (Transform child in transform)
        {
            var weapon = child.GetComponent<WeaponBehavior>();
            if (weapon != null)
            {
                currentChildren.Add(weapon);
            }
        }

        // Remove any old references that no longer exist in the hierarchy
        for (int i = _weaponBehaviors.Count - 1; i >= 0; i--)
        {
            WeaponBehavior oldWeapon = _weaponBehaviors[i];
            if (!currentChildren.Contains(oldWeapon))
            {
                _weaponBehaviors.RemoveAt(i);
                OnWeaponRemoved?.Invoke(oldWeapon);
                if (_attackTimers.ContainsKey(oldWeapon))
                {
                    Debug.Log($"Removing {oldWeapon.name} from the attack timers");
                    _attackTimers.Remove(oldWeapon);
                }
            }
        }

        // Add any new weapons not already in the list
        for (int i = 0; i < currentChildren.Count; i++)
        {
            WeaponBehavior newWeapon = currentChildren[i];
            if (!_weaponBehaviors.Contains(newWeapon))
            {
                _weaponBehaviors.Add(newWeapon);
                OnWeaponAdded?.Invoke(newWeapon);
            }
        }
    }

    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;

    void ExecuteAttackOfWeapon(WeaponBehavior weaponBehavior)
    {
        // If the weapon isn't tracked yet, add a new AttackTimer
        if (!_attackTimers.TryGetValue(weaponBehavior, out AttackTimer attackTimer))
        {
            attackTimer = new AttackTimer(0f, weaponBehavior.Stats.cooldown);
            _attackTimers.Add(weaponBehavior, attackTimer);
            Debug.Log($"Added {weaponBehavior.name} to the attack timers");
        }

        // Reduce the time until next attack
        attackTimer.nextAttackTime -= Time.deltaTime;

        // If it's ready to attack, fire and reset the cooldown
        if (attackTimer.nextAttackTime <= 0f)
        {
            weaponBehavior.Fire(attacker);
            attackTimer.nextAttackTime = attackTimer.cooldown;
        }

        // Update the timer reference
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
