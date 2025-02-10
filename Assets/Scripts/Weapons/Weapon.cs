using UnityEngine;

public abstract class Weapon : Upgradable<WeaponStats>
{
    public WeaponStats poweredUpStats;

    public abstract void Fire(GameObject o);
}
