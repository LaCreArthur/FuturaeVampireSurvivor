using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelUpChoiceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] TextMeshProUGUI descriptionTMP;
    [SerializeField] Image image;
    [SerializeField] GameObject newTag;
    Button _button;
    LevelUpUI _levelUpUI;

    public static event Action OnUpgrade;

    void Awake() => _button = GetComponent<Button>();

    public void SetData(UpgradableSO upgradable, int level)
    {
        nameTMP.text = upgradable.name;
        descriptionTMP.text = upgradable.GetUpgradeDescription(level);
        image.sprite = upgradable.sprite;
        newTag.SetActive(level == -1);
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => OnClick(upgradable));
    }

    void OnClick(UpgradableSO upgradable)
    {
        Debug.Log($"clicked {upgradable.name}");
        PlayerUpgradables.AddOrUpgrade(upgradable);
        OnUpgrade?.Invoke();
    }
}
