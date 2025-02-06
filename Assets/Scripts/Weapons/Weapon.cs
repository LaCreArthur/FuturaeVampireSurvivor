using UnityEngine;

public abstract class Weapon : Upgradable
{
    [SerializeField] UpgradableLevelData poweredUpStats;

    public UpgradableLevelData PoweredUpStats
    {
        get => poweredUpStats;
        private set => poweredUpStats = value;
    }

    protected virtual void Start()
    {
        PlayerUpgradables.OnUpgradableAdded += ApplyPowerUps;
        OnUpgrade += ApplyPowerUps;
    }

    void OnDestroy()
    {
        PlayerUpgradables.OnUpgradableAdded -= ApplyPowerUps;
        OnUpgrade -= ApplyPowerUps;
    }

    public void ApplyPowerUps() => ApplyPowerUps(null);

    public abstract void Fire(GameObject o);

    void ApplyPowerUps(UpgradableSO _ = null)
    {
        // copy the stats to avoid modifying the original
        UpgradableLevelData poweredUpData = Stats;

        foreach (PowerUp p in PlayerUpgradables.PowerUps)
        {
            poweredUpData = p.Apply(poweredUpData);
        }
        PoweredUpStats = poweredUpData;
        Debug.Log($"Weapon: Applied PowerUps to {upgradable.name}");
    }
}
