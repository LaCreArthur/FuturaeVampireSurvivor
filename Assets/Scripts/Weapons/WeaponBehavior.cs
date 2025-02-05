using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    public WeaponSO weapon;
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
        MaxLevel = weapon.levelData.Length - 1;
        Stats = weapon.levelData[CurrentLevel];
    }

    public virtual void Fire(GameObject o) => Debug.Log($"{weapon.name} (Level {CurrentLevel + 1}) fired with damage {Stats.damage}");

    public void UpgradeWeapon()
    {
        if (CurrentLevel < weapon.levelData.Length - 1)
        {
            Debug.Log($"WeaponBehavior: Upgraded {weapon.name} to Level {CurrentLevel + 1}");
            CurrentLevel++;
            Stats = weapon.levelData[CurrentLevel];
        }
    }
}
