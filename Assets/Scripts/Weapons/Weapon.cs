using UnityEngine;

public abstract class Weapon : Upgradable<WeaponStats>
{
    public WeaponStats modifiedStats;

    public abstract void Fire(GameObject o);
}
