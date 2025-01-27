using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] WeaponSO weapon;
    float _nextAttackTime;

    void Start() => _nextAttackTime = weapon.cooldown;

    void Update()
    {
        _nextAttackTime -= Time.deltaTime;
        if (_nextAttackTime <= 0)
        {
            weapon.ExecuteAttack(gameObject);
            _nextAttackTime = weapon.cooldown;
        }
    }
}
