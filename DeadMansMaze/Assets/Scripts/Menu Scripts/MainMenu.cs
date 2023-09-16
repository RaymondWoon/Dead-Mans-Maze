using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _confirmationPanel;
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private GameObject _instructionsPanel;

    [Header("Maze Parameters")]
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private InputField _widthInputField;
    [SerializeField] private Slider _depthSlider;
    [SerializeField] private InputField _depthInputField;
    [SerializeField] private Slider _enemySlider;
    [SerializeField] private InputField _enemyInputField;

    [Header("Audio Controls")]
    [SerializeField] private AudioMixer _mixer;
    //[SerializeField] private GameObject _window;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _musicSlider;

    [Header("Mouse Control")]
    [SerializeField] private Slider _mouseSlider;
    [SerializeField] private InputField _mouseInputField;

    [Header("Completion Time")]
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private InputField _timeInputField;

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the maze width input field
        _widthInputField.onValueChanged.AddListener(delegate { WidthInputValueChanged(); });

        // Add a listener to the maze depth input field
        _depthInputField.onValueChanged.AddListener(delegate { DepthInputValueChanged(); });

        // Add a listener to the number of enemies input field
        _enemyInputField.onValueChanged.AddListener(delegate { EnemyInputValueChanged(); });

        // Add a listener to the time input field
        _timeInputField.onValueChanged.AddListener(delegate { TimeInputValueChanged(); });

        // Add a listener to the mouse input field
        _mouseInputField.onValueChanged.AddListener(delegate { MouseSensitivityInputValueChanged(); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnHelpButton();
        }
    }

    public void DisableAudioSettingsPanel()
    {
        _audioPanel.SetActive(false);
    }

    public void DisableConfirmationPanel()
    {
        _confirmationPanel.SetActive(false);
    }

    public void DisableInstructionPanel()
    {
        _instructionsPanel.SetActive(false);
    }

    public void DisableOptionPanel()
    {
        _optionsPanel.SetActive(false);
    }

    public void EnableConfirmationPanel()
    {
        _confirmationPanel.SetActive(true);
    }

    public void OnApplyButton()
    {
        MainManager.Instance.MazeWidth = Convert.ToInt32(_widthInputField.text);
        MainManager.Instance.MazeDepth = Convert.ToInt32(_depthInputField.text);
        MainManager.Instance.NumberOfEnemies = Convert.ToInt32(_enemyInputField.text);
        MainManager.Instance.TimeToComplete = Convert.ToInt32(_timeInputField.text);
        MainManager.Instance.MouseSensitivity = (float) Convert.ToDouble(string.Format("{0:F2}", _mouseInputField.text));

        DisableOptionPanel();
    }

    public void OnAudioSettingsButton()
    {
        // activate the 'Audio Settings Panel' 
        _audioPanel.SetActive(true);

        // Initiate the Master Volume audio & slider to the Master Volume PlayerPref parameter
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            _mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            // set slider values based on current preference
            _masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }

        // Initiate the SFX audio & slider to the SFX PlayerPref parameter
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        // Initiate the Music audio & slider to the Music PlayerPref parameter
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    public void OnOptionsButton()
    {
        // activate 'Options Panel'
        _optionsPanel.SetActive(true);

        // set the width slider and text to the current maze width value
        _widthSlider.value = MainManager.Instance.MazeWidth;
        _widthInputField.text = MainManager.Instance.MazeWidth.ToString();

        // set the depth slider and text to the current maze depth value
        _depthSlider.value = MainManager.Instance.MazeDepth;
        _depthInputField.text = MainManager.Instance.MazeWidth.ToString();

        // set the enemies slider and text to the current enemies value
        _enemySlider.value = MainManager.Instance.NumberOfEnemies;
        _enemyInputField.text = MainManager.Instance.NumberOfEnemies.ToString();

        // set the time slider and text to the current timeToComplete value
        _timeSlider.value = MainManager.Instance.TimeToComplete;
        _timeInputField.text = MainManager.Instance.TimeToComplete.ToString();

        // set the mouse slider and text to the current mouseSensitivity value
        _mouseSlider.value = MainManager.Instance.MouseSensitivity;
        //_mouseInputField.text = MainManager.Instance.MouseSensitivity.ToString();
        _mouseInputField.text = string.Format("{0:F2}", MainManager.Instance.MouseSensitivity);

        // width slider event listener
        _widthSlider.onValueChanged.AddListener((v) =>
        {
            // update the width input field text
            _widthInputField.text = v.ToString("0");
        });

        // depth slider event listener
        _depthSlider.onValueChanged.AddListener((v) =>
        {
            // update the depth input field text
            _depthInputField.text = v.ToString("0");
        });

        // enemy slider event listener
        _enemySlider.onValueChanged.AddListener((v) =>
        {
            // update the enemy input field text
            _enemyInputField.text = v.ToString("0");
        });

        // time slider event listener
        _timeSlider.onValueChanged.AddListener((v) =>
        {
            // update the time input field text
            _timeInputField.text = v.ToString("0");
        });

        // mouse slider event listener
        _mouseSlider.onValueChanged.AddListener((v) =>
        {
            // update the mouse input field text
            _mouseInputField.text = string.Format("{0:F2}", v);
        });
    }

    public void OnHelpButton()
    {
        // activate 'Instructions Panel'
        _instructionsPanel.SetActive(true);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButton()
    {
#if UNITY_EDITOR
        // Stops the Unity engine
        EditorApplication.isPlaying = false;
#endif
        // Quit the application
        Application.Quit();
    }

    // Handler for MazeWidthInputField
    private void WidthInputValueChanged()
    {
        int newValue = Convert.ToInt32(_widthInputField.text);

        if (newValue >= MainManager.Instance.MinMazeWidth && newValue <= MainManager.Instance.MaxMazeWidth)
        {
            _widthSlider.value = newValue;
        }
        else if (newValue < MainManager.Instance.MinMazeWidth)
        {
            _widthSlider.value = MainManager.Instance.MinMazeWidth;

        }
        else
        {
            _widthSlider.value = MainManager.Instance.MaxMazeWidth;
        }
    }

    // Handler for MazeDepthInputField
    private void DepthInputValueChanged()
    {
        int newValue = Convert.ToInt32(_depthInputField.text);

        if (newValue >= MainManager.Instance.MinMazeDepth && newValue <= MainManager.Instance.MaxMazeDepth)
        {
            _depthSlider.value = newValue;
        }
        else if (newValue < MainManager.Instance.MinMazeDepth)
        {
            _depthSlider.value = MainManager.Instance.MinMazeDepth;

        }
        else
        {
            _depthSlider.value = MainManager.Instance.MaxMazeDepth;
        }
    }

    // Handler for EnemyInputField
    private void EnemyInputValueChanged()
    {
        int newValue = Convert.ToInt32(_enemyInputField.text);

        if (newValue >= MainManager.Instance.MinNumEnemies && newValue <= MainManager.Instance.MaxNumEnemies)
        {
            _enemySlider.value = newValue;
        }
        else if (newValue < MainManager.Instance.MinNumEnemies)
        {
            _enemySlider.value = MainManager.Instance.MinNumEnemies;

        }
        else
        {
            _enemySlider.value = MainManager.Instance.MaxNumEnemies;
        }
    }

    // Handler for TimeInputField
    private void TimeInputValueChanged()
    {
        int newValue = Convert.ToInt32(_timeInputField.text);

        if (newValue >= MainManager.Instance.MinTimeToComplete && newValue <= MainManager.Instance.MaxTimeToComplete)
        {
            _timeSlider.value = newValue;
        }
        else if (newValue < MainManager.Instance.MinTimeToComplete)
        {
            _timeSlider.value = MainManager.Instance.MinTimeToComplete;
        }
        else
        {
            _timeSlider.value = MainManager.Instance.MaxTimeToComplete;
        }
    }

    // Handler for MouseInputField
    private void MouseSensitivityInputValueChanged()
    {
        float newValue = (float) Convert.ToDouble(_mouseInputField.text);

        if (newValue >= MainManager.Instance.MinMouseSensitivity && newValue <= MainManager.Instance.MaxMouseSensitivity)
        {
            _mouseSlider.value = newValue;
        }
        else if (newValue < MainManager.Instance.MinMouseSensitivity)
        {
            _mouseSlider.value = MainManager.Instance.MinMouseSensitivity;
        }
        else
        {
            _mouseSlider.value = MainManager.Instance.MaxMouseSensitivity;
        }
    }

    // Update the AudioMixer 'Master Volume' and PlayerPrefs
    public void UpdateMasterVolume()
    {
        _mixer.SetFloat("MasterVolume", _masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", _masterSlider.value);
    }

    // Update the AudioMixer 'SFX Volume' and PlayerPrefs
    public void UpdateSFXVolume()
    {
        _mixer.SetFloat("SFXVolume", _sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
    }

    // Update the AudioMixer 'Music Volume' and PlayerPrefs
    public void UpdateMusicVolume()
    {
        _mixer.SetFloat("MusicVolume", _musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
    }
}
