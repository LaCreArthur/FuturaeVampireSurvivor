using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] WeaponBehavior weaponBehavior;

    float _nextAttackTime;
    float _cooldown;

    void Start()
    {
        if (weaponBehavior == null)
        {
            Debug.LogWarning("No WeaponBehavior set in WeaponSystem", this);
            enabled = false;
        }
        else
        {
            _cooldown = _nextAttackTime = weaponBehavior.weaponData.cooldown;
        }
    }

    void Update()
    {
        _nextAttackTime -= Time.deltaTime;
        if (_nextAttackTime <= 0)
        {
            weaponBehavior.ExecuteAttack(gameObject);
            _nextAttackTime = _cooldown;
        }
    }
}
