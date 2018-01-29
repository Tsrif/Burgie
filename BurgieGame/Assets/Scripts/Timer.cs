using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour {

    private Text timerText;
    public Text WinScreenTime;
    public float timer = 0;
    private bool stop;
    private bool paused;

	void Start () {
        timerText = GetComponent<Text>();
        stop = false;
	}

    private void OnEnable()
    {
        EndOfLevel.winner += timeStop; //subscribe 
        GameController.currentGameState += Pause;
    }

    private void OnDisable()
    {
        EndOfLevel.winner -= timeStop;
        GameController.currentGameState -= Pause;
    }


    private void FixedUpdate() {
        if (!stop && !paused )
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            int miliseconds = Mathf.FloorToInt(timer * 100);
            miliseconds = miliseconds % 100;
            string formattedTime = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, miliseconds); //change time format to minutes:seconds:miliseconds
            timerText.text = formattedTime;
        }
	}

    private void Pause(GameState gameState)
    {
        if (gameState == GameState.MENU)
        {
            paused = true;
        }
        else { paused = false; }
    }

    //Stop the timer and time on text
    void timeStop() {
        stop = true;
        WinScreenTime.text = "Your Time: " + timerText.text;
    }

    
}
