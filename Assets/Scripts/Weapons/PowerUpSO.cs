using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/PowerUp", fileName = "PowerUpSO")]
public class PowerUpSO : UpgradableSO<Modifiers>
{
    public override string GetUpgradeDescription(int level)
    {
        if (level + 1 >= levelData.Length)
        {
            return "Max level reached, you should not see this!";
        }
        return baseDescription;
    }
}
