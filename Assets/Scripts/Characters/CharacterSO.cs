using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Vampire/Character", fileName = "CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject
{
    public Sprite sprite;
    public GameObject prefab;
    public WeaponSO weaponSO;
    public Modifiers baseStats;
}
