using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;

    int _currentCharacterIndex;

    void Awake()
    {
        foreach (GameObject character in characters)
            character.SetActive(false);

        _currentCharacterIndex = 0;
        characters[_currentCharacterIndex].SetActive(true);
    }

    void OnEnable() => ButtonSwitch.OnSwitch += SwitchCharacter;

    void OnDisable() => ButtonSwitch.OnSwitch -= SwitchCharacter;

    void SwitchCharacter(bool previous)
    {
        characters[_currentCharacterIndex].SetActive(false);

        _currentCharacterIndex += previous ? -1 : 1;

        if (_currentCharacterIndex < 0)
            _currentCharacterIndex = characters.Count - 1;
        else if (_currentCharacterIndex >= characters.Count)
            _currentCharacterIndex = 0;

        characters[_currentCharacterIndex].SetActive(true);
    }
}
