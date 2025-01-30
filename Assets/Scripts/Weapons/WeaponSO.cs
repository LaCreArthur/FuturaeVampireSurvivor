using UnityEngine;

[CreateAssetMenu(menuName = "Create WeaponSO", fileName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public Sprite weaponIcon;

    public WeaponLevelData[] levelData;

    public AudioClip fireSound;
    public GameObject projectilePrefab;
}
