using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _confirmationPanel;
    [SerializeField] private GameObject _instructionsPanel;

    [Header("Maze Parameters")]
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private InputField _widthInputField;
    [SerializeField] private Slider _depthSlider;
    [SerializeField] private InputField _depthInputField;
    [SerializeField] private Slider _enemySlider;
    [SerializeField] private InputField _enemyInputField;

    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the maze width input field
        _widthInputField.onValueChanged.AddListener(delegate { WidthInputValueChanged(); });

        // Add a listener to the maze depth input field
        _depthInputField.onValueChanged.AddListener(delegate { DepthInputValueChanged(); });

        // Add a listener to the number of enemies input field
        _enemyInputField.onValueChanged.AddListener(delegate { EnemyInputValueChanged(); });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnHelpButton();
        }
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

        // this is duplication and may have been conflicting with the other listeners
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

}
