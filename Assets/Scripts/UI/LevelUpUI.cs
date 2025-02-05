using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] GameObject choicePrefab;
    [SerializeField] List<WeaponSO> weapons;
    [SerializeField] PlayerWeapons playerWeapons;

    readonly List<LevelUpChoiceUI> _choices = new List<LevelUpChoiceUI>();

    void OnEnable()
    {
        DespawnOldChoices();
        SpawnChoices();
    }

    void DespawnOldChoices()
    {
        foreach (LevelUpChoiceUI choice in _choices)
        {
            PoolManager.Despawn(choice.gameObject);
        }
        _choices.Clear();
    }

    void SpawnChoices()
    {
        List<WeaponSO> choiceWeapon = weapons.GetRandoms(3, true);
        for (int i = 0; i < 3; i++)
        {
            // if the max level is reached, don't show the weapon
            WeaponBehavior weaponBehavior = playerWeapons.GetWeaponBehavior(choiceWeapon[i]);
            int currentLevel = weaponBehavior != null ? weaponBehavior.CurrentLevel : -1;
            if (currentLevel == weaponBehavior?.MaxLevel) continue;

            Debug.Log($"SpawnChoices: {choiceWeapon[i].name}, cl: {currentLevel}, ml: {weaponBehavior?.MaxLevel}");

            GameObject choiceGO = PoolManager.Spawn(choicePrefab, Vector3.zero, Quaternion.identity, transform);
            var choiceUI = choiceGO.GetComponent<LevelUpChoiceUI>();
            choiceUI.SetData(choiceWeapon[i], currentLevel);
            _choices.Add(choiceUI);
        }
    }

    public void UpgradeWeapon(WeaponSO weapon) => playerWeapons.UpgradeWeapon(weapon);
}
