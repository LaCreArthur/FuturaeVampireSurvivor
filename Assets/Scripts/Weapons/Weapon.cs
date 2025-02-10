using UnityEngine;

public abstract class Weapon : Upgradable
{

    public WeaponStats poweredUpStats;

    public abstract void Fire(GameObject o);
}
