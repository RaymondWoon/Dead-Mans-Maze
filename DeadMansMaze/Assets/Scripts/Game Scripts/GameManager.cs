using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool _gamePaused;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GunPlay._gameIsPaused = !GunPlay._gameIsPaused;
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        _gamePaused = !_gamePaused;

        // Pause/unpause the game in unity
        Time.timeScale = _gamePaused == true ? 0.0f : 1.0f;

        // set the cursor mode
        Cursor.lockState = _gamePaused ? CursorLockMode.None : CursorLockMode.Locked;

        // toggle the pause menu
        UIManager.instance.TogglePauseMenu(_gamePaused);
    }
}
