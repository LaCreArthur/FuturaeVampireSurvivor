using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSwitch : MonoBehaviour
{
    [SerializeField] bool previous;
    Button _button;

    public static event Action<bool> OnSwitch;
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(() => OnSwitch?.Invoke(previous));
    }
}
