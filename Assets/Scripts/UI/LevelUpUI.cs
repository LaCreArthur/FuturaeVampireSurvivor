using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] GameObject choicePrefab;
    [SerializeField] List<WeaponSO> weapons;
    [SerializeField] PlayerWeapons playerWeapons;

    void OnEnable()
    {
        DespawnOldChoices();
        SpawnChoices();
    }

    void DespawnOldChoices()
    {
        foreach (Transform child in transform)
        {
            PoolManager.Despawn(child.gameObject);
        }
    }

    void SpawnChoices()
    {
        List<WeaponSO> choiceWeapon = weapons.GetRandoms(3, true);
        for (int i = 0; i < 3; i++)
        {
            GameObject choiceGO = PoolManager.Spawn(choicePrefab, Vector3.zero, Quaternion.identity, transform);
            var choiceUI = choiceGO.GetComponent<LevelUpChoiceUI>();
            int weaponLevel = playerWeapons.GetWeaponLevel(choiceWeapon[i]);
            choiceUI.SetData(choiceWeapon[i], weaponLevel);
        }
    }

    public void UpgradeWeapon(WeaponSO weapon) => playerWeapons.UpgradeWeapon(weapon);
}
