using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    public WeaponSO weapon;

    int _currentLevel;

    WeaponLevelData? _stats;
    public WeaponLevelData Stats => _stats ??= weapon.levelData[_currentLevel];


    public virtual void Fire(GameObject o) => Debug.Log($"{weapon.name} (Level {_currentLevel + 1}) fired with damage {Stats.damage}");

    public void UpgradeWeapon()
    {
        if (_currentLevel < weapon.levelData.Length - 1)
        {
            _currentLevel++;
            Debug.Log($"Upgraded {weapon.name} to Level {_currentLevel + 1}");
        }
    }
}
