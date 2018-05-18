using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSound : MonoBehaviour {
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private void OnEnable()
    {
        GameController.currentGameState += Pause;

    }

    private void OnDisable()
    {
        GameController.currentGameState -= Pause;
    }

    public void Pause(GameState gameState)
    {
        if (gameState == GameState.MENU)
        {

            source.Pause();
        }
        else { source.UnPause(); }
      
    }
   
}
