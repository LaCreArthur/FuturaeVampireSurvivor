﻿using UnityEngine;
using UnityEngine.UI;

public class UpgradableSpotsUI : MonoBehaviour
{
    [SerializeField] Image[] weaponImages = new Image[4];
    [SerializeField] Image[] powerUpImages = new Image[4];

    int _weaponIndex;
    int _powerUpIndex;

    void Awake()
    {
        PlayerEquipment.OnUpgradableAdded += UpdateSpot;
        GameStateManager.OnPlaying += ResetUI;
        ResetUI();
    }

    void ResetUI()
    {
        _weaponIndex = 0;
        _powerUpIndex = 0;
        foreach (Image w in weaponImages)
            w.enabled = false;
        foreach (Image p in powerUpImages)
            p.enabled = false;

        PlayerEquipment.Weapons.ForEach(w => UpdateSpot(w.upgradable));
    }

    void UpdateSpot(UpgradableSO obj)
    {
        if (obj.isPowerUp)
        {
            powerUpImages[_powerUpIndex].enabled = true;
            powerUpImages[_powerUpIndex].sprite = obj.sprite;
            _powerUpIndex++;
        }
        else
        {
            weaponImages[_weaponIndex].enabled = true;
            weaponImages[_weaponIndex].sprite = obj.sprite;
            _weaponIndex++;
        }
    }
}
