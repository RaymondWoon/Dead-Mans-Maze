using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private TMP_Text _map;
    

    [Header("Paused Menu")]
    [SerializeField] private GameObject _pauseMenu;

    [HideInInspector]
    public static UIManager instance;

    // Variables
    private bool _showMap;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _showMap = false;
        ToggleMapDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _showMap = !_showMap;

            ToggleMapDisplay();
        }
    }

    private void ToggleMapDisplay()
    {
        _map.enabled = _showMap;
    }

    public void TogglePauseMenu(bool paused)
    {
        _pauseMenu.SetActive(paused);
    }

}
