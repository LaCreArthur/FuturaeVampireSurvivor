using UnityEngine;

[CreateAssetMenu(menuName = "Create CharacterSO", fileName = "CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject
{
    public Sprite characterIcon;
    public GameObject characterPrefab;
    public GameObject baseWeaponPrefab;
}
