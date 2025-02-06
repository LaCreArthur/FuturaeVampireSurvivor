using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] GameObject attacker;

    // Tracks how long until each weapon can attack again
    readonly Dictionary<Weapon, AttackTimer> _attackTimers = new Dictionary<Weapon, AttackTimer>();
    static List<Weapon> Weapons => PlayerUpgradables.Weapons;

    void Start()
    {
        GameStateManager.OnStateChange += OnStateChanged;
        OnStateChanged(GameStateManager.CurrentState);
        PlayerUpgradables.OnUpgradableAdded += OnUpgradableAdded;
        Upgradable.OnUpgrade += ResetTimers;
    }

    void Update()
    {
        // Exits if no weapons
        if (Weapons.Count == 0) return;


        // Iterate through each weapon and process attacks
        foreach (Weapon weapon in Weapons)
        {
            if (!PlayerUpgradables.Weapons.Contains(weapon))
                _attackTimers.Remove(weapon);
            ExecuteAttackOfWeapon(weapon);
        }
    }

    void OnDestroy()
    {
        GameStateManager.OnStateChange -= OnStateChanged;
        PlayerUpgradables.OnUpgradableAdded -= OnUpgradableAdded;
        Upgradable.OnUpgrade -= ResetTimers;
    }
    void ResetTimers()
    {
        foreach (Weapon weapon in Weapons)
        {
            if (_attackTimers.TryGetValue(weapon, out AttackTimer attackTimer))
            {
                attackTimer.nextAttackTime = 0f;
                attackTimer.cooldown = weapon.PoweredUpStats.cooldown;
                Debug.Log($"updated {weapon.name} attack timers with a cooldown of {attackTimer.cooldown}");
                _attackTimers[weapon] = attackTimer;
            }
        }
    }

    void OnUpgradableAdded(UpgradableSO upgradableSO)
    {
        GameObject go = Instantiate(upgradableSO.prefab, transform.position, Quaternion.identity, transform);
        var upgradable = go.GetComponent<Upgradable>();
        PlayerUpgradables.AddUpgradable(upgradable);
    }

    void OnStateChanged(GameState state) => enabled = state == GameState.Playing;

    void ExecuteAttackOfWeapon(Weapon weapon)
    {
        // If the weapon isn't tracked yet, add a new AttackTimer
        if (!_attackTimers.TryGetValue(weapon, out AttackTimer attackTimer))
        {
            attackTimer = new AttackTimer(0f, weapon.PoweredUpStats.cooldown);
            _attackTimers.Add(weapon, attackTimer);
            Debug.Log($"Added {weapon.name} to the attack timers with a cooldown of {attackTimer.cooldown}");
        }

        // Reduce the time until next attack
        attackTimer.nextAttackTime -= Time.deltaTime;

        // If it's ready to attack, fire and reset the cooldown
        if (attackTimer.nextAttackTime <= 0f)
        {
            weapon.Fire(attacker);
            attackTimer.nextAttackTime = attackTimer.cooldown;
        }

        // Update the timer reference
        _attackTimers[weapon] = attackTimer;
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
