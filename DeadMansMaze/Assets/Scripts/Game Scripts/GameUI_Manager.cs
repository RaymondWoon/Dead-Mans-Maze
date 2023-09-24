using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI_Manager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private Text _scoreText;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private TMP_Text _map;


    [Header("Menu")]
    [SerializeField] private GameObject _pause_ui;
    [SerializeField] private GameObject _won_ui;
    [SerializeField] private GameObject _loss_ui;

    [Header("Miscellaneous")]
    [SerializeField] private GameManager2 _gameManager;
    [SerializeField] private Camera _auxCam;

    [HideInInspector]
    public static GameUI_Manager instance;

    // Variables
    private bool _showMap;

    public enum GameUI_State
    {
        GamePlay,
        Pause,
        GameOver
    }

    private GameObject _player;

    public GameUI_State _currentState;

    private void Awake()
    {
        instance = this;
        _player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Default to GamePlay
        SwitchUIState(GameUI_State.GamePlay);
        // Default, hide map
        _showMap = false;
        ToggleMapDisplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _showMap = !_showMap;

            ToggleMapDisplay();
        }

        // ESC toggles the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseUI();

        if (_currentState == GameUI_State.GamePlay)
            UpdateHealthBar(PlayerStatus.currentHp, PlayerStatus.maxHp);

        if (_currentState == GameUI_State.GameOver)
            SwitchUIState(GameUI_State.GameOver);
    }

    private void SwitchUIState(GameUI_State state)
    {
        _pause_ui.SetActive(false);

        Time.timeScale = 1.0f;

        switch (state)
        {
            case GameUI_State.GamePlay:
                _player.SetActive(true);
                _auxCam.enabled = false;
                _auxCam.GetComponent<AudioListener>().enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;

            case GameUI_State.Pause:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                _player.SetActive(false);
                _auxCam.enabled = true;
                _auxCam.GetComponent<AudioListener>().enabled = true;
                _pause_ui.SetActive(true);
                break;

            case GameUI_State.GameOver:
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _player.SetActive(false);
                _auxCam.enabled = true;
                _auxCam.GetComponent<AudioListener>().enabled = true;
                _loss_ui.SetActive(true);
                break;
        }

        _currentState = state;
    }

    public void TogglePauseUI()
    {
        if (_currentState == GameUI_State.GamePlay)
        {
            SwitchUIState(GameUI_State.Pause);
        }
        else if (_currentState == GameUI_State.Pause)
        {
            SwitchUIState(GameUI_State.GamePlay);
        }
    }

    public void Button_MainMenu()
    {
        _gameManager.ReturnToMainMenu();
    }

    public void Button_Restart()
    {
        _gameManager.Restart();
    }

    private void ToggleMapDisplay()
    {
        _map.enabled = _showMap;
    }

    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        // return a value between 0 and 1
        _healthBarFill.fillAmount = (float)currentHP / (float)maxHP;
    }

}
