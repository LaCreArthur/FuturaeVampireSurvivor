using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    public UpgradableSO upgradable;
    [SerializeField] [ReadOnly] UpgradableLevelData stats;
    public int CurrentLevel { get; private set; }
    public int MaxLevel { get; private set; }
    public UpgradableLevelData Stats
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

    public virtual void Fire(GameObject o) => Debug.Log($"{upgradable.name} (Level {CurrentLevel + 1}) fired with damage {Stats.damage}");

    public void UpgradeWeapon()
    {
        if (CurrentLevel < upgradable.levelData.Length - 1)
        {
            Debug.Log($"WeaponBehavior: Upgraded {upgradable.name} to Level {CurrentLevel + 1}");
            CurrentLevel++;
            Stats = upgradable.levelData[CurrentLevel];
        }
    }
}
