using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] GameObject choicePrefab;
    [SerializeField] List<UpgradableSO> upgradables;

    readonly List<LevelUpChoiceUI> _choices = new List<LevelUpChoiceUI>();

    void OnEnable()
    {
        ClearChoices();
        CreateNewChoices();
    }

    void ClearChoices()
    {
        foreach (LevelUpChoiceUI choice in _choices)
            PoolManager.Despawn(choice.gameObject);
        _choices.Clear();
    }

    void CreateNewChoices()
    {
        List<UpgradableSO> validChoices = GetValidChoices();
        List<UpgradableSO> selections = validChoices.GetRandoms(Mathf.Min(3, validChoices.Count), true);

        foreach (UpgradableSO selection in selections)
        {
            var choiceUI = PoolManager.Spawn(choicePrefab, Vector3.zero, Quaternion.identity, transform)
                .GetComponent<LevelUpChoiceUI>();

            choiceUI.SetData(selection,
                PlayerEquipment.GetInstance(selection)?.CurrentLevel ?? -1);

            _choices.Add(choiceUI);
        }
    }

    List<UpgradableSO> GetValidChoices() => upgradables.Where(u =>
    {
        Upgradable instance = PlayerEquipment.GetInstance(u);

        // Already equipped - check if upgradeable
        if (instance != null)
            return instance.CurrentLevel < instance.MaxLevel;

        // Not equipped - check slot availability
        return u is PowerUpSO ? PlayerEquipment.CanAddPowerUp : PlayerEquipment.CanAddWeapon;
    }).ToList();
}
