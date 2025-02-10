using UnityEngine;

public abstract class Upgradable<T> : Upgradable
{
    public UpgradableSO<T> upgradable;
    [SerializeField] protected T stats;

    public T Stats
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

    public override void Upgrade()
    {
        if (CurrentLevel < upgradable.levelData.Length - 1)
        {
            Debug.Log($"Upgradable: Upgraded {upgradable.name} to Level {CurrentLevel + 1}");
            CurrentLevel++;
            Stats = upgradable.levelData[CurrentLevel];
        }
    }
}

public abstract class Upgradable : MonoBehaviour
{
    public int CurrentLevel { get; protected set; }
    public int MaxLevel { get; protected set; }
    public abstract void Upgrade();
}
