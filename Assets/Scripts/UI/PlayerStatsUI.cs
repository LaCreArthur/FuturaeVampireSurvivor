using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    public TextMeshProUGUI maxHealthTMP;
    public TextMeshProUGUI recoveryTMP;
    public TextMeshProUGUI armorTMP;
    public TextMeshProUGUI moveSpeedTMP;
    public TextMeshProUGUI growthTMP;
    public TextMeshProUGUI attractionRangeTMP;
    public TextMeshProUGUI mightTMP;
    public TextMeshProUGUI areaTMP;
    public TextMeshProUGUI speedTMP;
    public TextMeshProUGUI amountTMP;
    public TextMeshProUGUI cooldownTMP;

    void Awake() => ModifierSystem.CharacterModifiersUpdated += UpdateStats;

    void OnDestroy() => ModifierSystem.CharacterModifiersUpdated -= UpdateStats;
    void UpdateStats(Modifiers m)
    {
        maxHealthTMP.text = m.maxHealth.ToString();
        recoveryTMP.text = m.recovery.ToString("F2");
        armorTMP.text = m.armor.ToString();
        moveSpeedTMP.text = m.moveSpeed.ToString("F0");
        growthTMP.text = m.growth.ToString();
        attractionRangeTMP.text = m.attractionRange.ToString("F0");
        mightTMP.text = m.might.ToString("F0");
        areaTMP.text = m.area.ToString("F0");
        speedTMP.text = m.speed.ToString("F0");
        amountTMP.text = m.amount.ToString();
        cooldownTMP.text = m.cooldown.ToString("F0");
    }
}
