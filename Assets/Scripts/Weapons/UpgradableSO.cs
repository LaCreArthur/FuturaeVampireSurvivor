using UnityEngine;

public abstract class UpgradableSO<T> : UpgradableSO
{
    public T[] levelData;
}

public abstract class UpgradableSO : ScriptableObject
{
    public Sprite sprite;
    public GameObject prefab;
    public string baseDescription;

    public abstract string GetUpgradeDescription(int level);
}
