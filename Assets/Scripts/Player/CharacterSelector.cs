using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] List<CharacterSO> characters;
    [SerializeField] Transform characterParent;
    [SerializeField] Transform weaponParent;

    readonly Vector3 _characterOffset = new Vector3(0, -.5f, 0);

    int _currentIndex;
    GameObject _currentCharacter;
    GameObject _currentWeapon;

    void Awake()
    {
        _currentIndex = 0;
        InstantiateCharacter();
    }

    void OnEnable() => ButtonSwitch.OnSwitch += SwitchCharacter;

    void OnDisable() => ButtonSwitch.OnSwitch -= SwitchCharacter;

    void SwitchCharacter(bool previous)
    {
        _currentIndex += previous ? -1 : 1;

        if (_currentIndex < 0)
            _currentIndex = characters.Count - 1;
        else if (_currentIndex >= characters.Count)
            _currentIndex = 0;

        InstantiateCharacter();
    }

    void InstantiateCharacter()
    {
        if (_currentCharacter) Destroy(_currentCharacter);
        if (_currentWeapon) Destroy(_currentWeapon);

        _currentCharacter = Instantiate(characters[_currentIndex].characterPrefab, _characterOffset, Quaternion.identity, characterParent);
        _currentWeapon = Instantiate(characters[_currentIndex].baseWeaponPrefab, Vector3.zero, Quaternion.identity, weaponParent);
    }
}
