﻿using UnityEngine;

public abstract class Upgradable : MonoBehaviour
{
    public UpgradableSO upgradable;
    [SerializeField] protected WeaponStats stats;

    public int CurrentLevel { get; private set; }
    public int MaxLevel { get; private set; }

    public WeaponStats Stats
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
            Debug.Log($"Upgradable: Upgraded {upgradable.name} to Level {CurrentLevel + 1}");
            CurrentLevel++;
            Stats = upgradable.levelData[CurrentLevel];
        }
    }
}
