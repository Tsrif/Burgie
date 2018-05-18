using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DeathCounter : MonoBehaviour {

    //create a singleton of the deathCounter 
    private static DeathCounter _instance;
    public int deathCount = 0;
    public static event Action<int> playerDied; //send notification to the save data to update totalDeaths

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void OnEnable()
    {
        Enemy.killPlayer += updateCounter;
        WaterSplash.drownPlayer += updateCounter;
        SceneManager.sceneLoaded += OnSceneLoaded; //scene manager notification that notifies when a new scene has been loaded
        Restarter.restart += updateCounter;
    }

    private void OnDisable()
    {
        Enemy.killPlayer -= updateCounter;
        WaterSplash.drownPlayer -= updateCounter;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Restarter.restart -= updateCounter;
    }

    void updateCounter() {
        deathCount++;
        playerDied(1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //we don't need the counter anymore if we go back to the title screen
        if (scene.name == "Title_Screen") {
            DestroyObject(gameObject);
        }
    }

    //shows the death count counter 
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 30), "Death Count: " + deathCount);
    }


}
