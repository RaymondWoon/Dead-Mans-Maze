using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    [SerializeField] private GameUI_Manager _gameUI_Manager;

    private bool _gameIsOver;

    public static GameManager2 instance;

    private void Awake()
    {
        // Initialize components
        instance = this;
        _gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // stop update if game is over
        if (_gameIsOver)
            return;

        // ESC toggles the pause menu
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    _gameUI_Manager.TogglePauseUI();
    }

    public void GameOver()
    {
        _gameIsOver = true;
    }

    public void LevelCompleted()
    {

    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void WinGame()
    {
        // set the end game screen
        UIManager.instance.SetEndGameScreen(true);
        // pause the game
        Time.timeScale = 0.0f;
        // unlock the cursor
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseGame()
    {
        // set the end game screen
        UIManager.instance.SetEndGameScreen(false);
        // pause the game
        Time.timeScale = 0.0f;
        // unlock the cursor
        Cursor.lockState = CursorLockMode.None;
    }

}
