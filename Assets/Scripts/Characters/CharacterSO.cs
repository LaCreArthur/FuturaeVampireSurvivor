using UnityEngine;

[CreateAssetMenu(menuName = "Vampire/Character", fileName = "CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject
{
    public Sprite characterIcon;
    public GameObject characterPrefab;
    public WeaponSO baseWeaponSO;
}
