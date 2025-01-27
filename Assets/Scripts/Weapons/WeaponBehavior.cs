using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    public WeaponDataSO weaponData;
    public abstract void ExecuteAttack(GameObject attacker);
}
