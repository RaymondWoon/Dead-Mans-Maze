using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Instruction : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _overviewButton;
    [SerializeField] private Button _configurationButton;
    [SerializeField] private Button _playerButton;
    [SerializeField] private Button _weaponButton;
    [SerializeField] private Button _pickupButton;

    [Header("Text Content")]
    [SerializeField] private TMP_Text _overviewText;
    [SerializeField] private TMP_Text _configurationText;
    [SerializeField] private TMP_Text _playerText;
    [SerializeField] private TMP_Text _weaponText;
    [SerializeField] private TMP_Text _pickupText;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial state
        _overviewButton.Select();
        // Disable all text
        DisableAllText();
        // enable overview by default
        _overviewText.enabled = true;
    }

    public void OnConfigurationButton()
    {
        // disable all text
        DisableAllText();

        // enable the configuration text
        _configurationText.enabled = true;
    }

    public void OnOverviewButton()
    {
        // disable all text
        DisableAllText();

        // enable the overview text
        _overviewText.enabled = true;
        
    }

    public void OnPickupButton()
    {
        // disable all text
        DisableAllText();

        // enable the pickup text
        _pickupText.enabled = true;
    }

    public void OnPlayerButton()
    {
        // disable all text
        DisableAllText();

        // enable the player text
        _playerText.enabled = true;
    }

    public void OnWeaponButton()
    {
        // disable all text
        DisableAllText();

        // enable the weapon text
        _weaponText.enabled = true;
    }

    // Disable all text
    private void DisableAllText()
    {
        _overviewText.enabled = false;
        _configurationText.enabled = false;
        _playerText.enabled = false;
        _weaponText.enabled = false;
        _pickupText.enabled = false;
    }
}
