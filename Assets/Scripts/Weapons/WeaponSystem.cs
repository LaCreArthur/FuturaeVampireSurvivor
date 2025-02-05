using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] GameObject attacker;

    // Tracks how long until each weapon can attack again
    readonly Dictionary<UpgradableBehavior, AttackTimer> _attackTimers = new Dictionary<UpgradableBehavior, AttackTimer>();

    // Tracks all active weapon behaviors on child objects
    readonly List<UpgradableBehavior> _weaponBehaviors = new List<UpgradableBehavior>();

    public static event Action<UpgradableBehavior> OnWeaponAdded;
    public static event Action<UpgradableBehavior> OnWeaponRemoved;

    void Start()
    {
        GameStateManager.OnStateChange += OnStateChanged;
        PlayerWeapons.OnNewWeapon += AddWeapon;
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
            UpgradableBehavior upgradableBehavior = _weaponBehaviors[i];

            // Skip inactive weapons
            if (!upgradableBehavior.gameObject.activeSelf) continue;

            ExecuteAttackOfWeapon(upgradableBehavior);
        }
    }

    void OnDestroy() => GameStateManager.OnStateChange -= OnStateChanged;

    void OnTransformChildrenChanged() => RefreshWeaponBehaviors();

    void RefreshWeaponBehaviors()
    {
        // Collect all relevant child WeaponBehaviors
        var currentChildren = new List<UpgradableBehavior>();
        foreach (Transform child in transform)
        {
            var weapon = child.GetComponent<UpgradableBehavior>();
            if (weapon != null)
            {
                currentChildren.Add(weapon);
            }
        }

        // Remove any old references that no longer exist in the hierarchy
        for (int i = _weaponBehaviors.Count - 1; i >= 0; i--)
        {
            UpgradableBehavior oldUpgradable = _weaponBehaviors[i];
            if (!currentChildren.Contains(oldUpgradable))
            {
                _weaponBehaviors.RemoveAt(i);
                OnWeaponRemoved?.Invoke(oldUpgradable);
                if (_attackTimers.ContainsKey(oldUpgradable))
                {
                    Debug.Log($"Removing {oldUpgradable.name} from the attack timers");
                    _attackTimers.Remove(oldUpgradable);
                }
            }
        }

        // Add any new weapons not already in the list
        for (int i = 0; i < currentChildren.Count; i++)
        {
            UpgradableBehavior newUpgradable = currentChildren[i];
            if (!_weaponBehaviors.Contains(newUpgradable))
            {
                _weaponBehaviors.Add(newUpgradable);
                OnWeaponAdded?.Invoke(newUpgradable);
            }
        }
    }

    void AddWeapon(UpgradableSO upgradable) => Instantiate(upgradable.prefab, transform.position, Quaternion.identity, transform);

    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;

    void ExecuteAttackOfWeapon(UpgradableBehavior upgradableBehavior)
    {
        // If the weapon isn't tracked yet, add a new AttackTimer
        if (!_attackTimers.TryGetValue(upgradableBehavior, out AttackTimer attackTimer))
        {
            attackTimer = new AttackTimer(0f, upgradableBehavior.Stats.cooldown);
            _attackTimers.Add(upgradableBehavior, attackTimer);
            Debug.Log($"Added {upgradableBehavior.name} to the attack timers");
        }

        // Reduce the time until next attack
        attackTimer.nextAttackTime -= Time.deltaTime;

        // If it's ready to attack, fire and reset the cooldown
        if (attackTimer.nextAttackTime <= 0f)
        {
            upgradableBehavior.Fire(attacker);
            attackTimer.nextAttackTime = attackTimer.cooldown;
        }

        // Update the timer reference
        _attackTimers[upgradableBehavior] = attackTimer;
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
