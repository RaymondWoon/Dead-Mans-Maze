using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _textTime;

    // Variables
    private float _timeRemaining = 300.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (MainManager.Instance)
        {
            // Time to complete is seconds
            _timeRemaining = (float)MainManager.Instance.TimeToComplete * 60.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else
        {
            _timeRemaining = 0.0f;
        }

        DisplayTime(_timeRemaining);

        if (_timeRemaining <= 0.0f)
            GameUI_Manager.instance._currentState = GameUI_Manager.GameUI_State.GameOver;
                
        //GameManager2.instance.LoseGame();
    }

    // Display in minutes and secconds
    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0.0f)
            timeToDisplay = 0;

        int minutes = (int)timeToDisplay / 60;
        int seconds = (int)timeToDisplay % 60;

        _textTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
