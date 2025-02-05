using UnityEngine;

public abstract class UpgradableBehavior : MonoBehaviour
{
    public UpgradableSO upgradable;
    [SerializeField] [ReadOnly] protected UpgradableLevelData stats;
    public int CurrentLevel { get; private set; }
    public int MaxLevel { get; private set; }
    public virtual UpgradableLevelData Stats
    {
        get => stats;
        private set => stats = value;
    }

    protected virtual void Awake()
    {
        CurrentLevel = 0;
        MaxLevel = upgradable.levelData.Length - 1;
        Stats = upgradable.levelData[CurrentLevel];
    }


    public void Upgrade()
    {
        if (CurrentLevel < upgradable.levelData.Length - 1)
        {
            Debug.Log($"WeaponBehavior: Upgraded {upgradable.name} to Level {CurrentLevel + 1}");
            CurrentLevel++;
            Stats = upgradable.levelData[CurrentLevel];
        }
    }
}

public abstract class WeaponBehavior : UpgradableBehavior
{
    public override UpgradableLevelData Stats => ApplyPowerUps(stats);

    UpgradableLevelData ApplyPowerUps(UpgradableLevelData data) =>
        // Apply powerups here
        // how to get power ups reference ?
        // I need a list of unlocked powerups
        data;

    public abstract void Fire(GameObject o);
}
