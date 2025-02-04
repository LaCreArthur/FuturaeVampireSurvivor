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

    public void SetData(WeaponSO weapon, int level)
    {
        nameTMP.text = weapon.name;
        descriptionTMP.text = weapon.GetUpgradeDescription(level);
        image.sprite = weapon.sprite;
        newTag.SetActive(level == 0);
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => OnClick(weapon));
    }

    void OnClick(WeaponSO weapon)
    {
        Debug.Log($"clicked {weapon.name}");
        // lazy load
        _levelUpUI ??= GetComponentInParent<LevelUpUI>(true);
        _levelUpUI.UpgradeWeapon(weapon);
        OnUpgrade?.Invoke();
    }
}
