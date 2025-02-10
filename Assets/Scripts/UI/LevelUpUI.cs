using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] GameObject choicePrefab;
    [SerializeField] List<UpgradableSO> upgradables;

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
        List<UpgradableSO> choices = upgradables.GetRandoms(3, true);
        for (int i = 0; i < 3; i++)
        {
            // if the max level is reached, don't show the weapon
            Upgradable upgradable = PlayerEquipment.GetInstance(choices[i]);
            int currentLevel = upgradable != null ? upgradable.CurrentLevel : -1;
            if (currentLevel == upgradable?.MaxLevel) continue;

            Debug.Log($"SpawnChoices: {choices[i].name}, cl: {currentLevel}, ml: {upgradable?.MaxLevel}");

            GameObject choiceGO = PoolManager.Spawn(choicePrefab, Vector3.zero, Quaternion.identity, transform);
            var choiceUI = choiceGO.GetComponent<LevelUpChoiceUI>();
            choiceUI.SetData(choices[i], currentLevel);
            _choices.Add(choiceUI);
        }
    }
}
