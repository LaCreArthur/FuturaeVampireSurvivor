using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextCharacterName : MonoBehaviour
{
    TextMeshProUGUI _text;
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        CharacterSelector.CharacterChanged += OnCharacterChanged;
    }

    void OnDestroy() => CharacterSelector.CharacterChanged -= OnCharacterChanged;
    void OnCharacterChanged(CharacterSO c) => _text.text = c.name;
}
