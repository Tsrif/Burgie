using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SaveData : MonoBehaviour {
    public PlayerSaveInfo _playerSaveInfo;
    //create a singleton of the saveData
    public static SaveData _instance;

    private void Awake()
    {
        //Create a singleton
        DontDestroyOnLoad(gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            LoadGame();
        }
    }

    private void OnEnable()
    {
        DeathCounter.playerDied += addDeath;
        Timer.levelTime += AddLevelAndTime; 
    }

    private void OnDisable()
    {
        DeathCounter.playerDied -= addDeath;
        Timer.levelTime -= AddLevelAndTime;
    }

    public void SaveGame()
    {
        ES3.Save<PlayerSaveInfo>("PlayerSaveData", _playerSaveInfo);
        
    }

    public void LoadGame()
    {
        ES3.Load<PlayerSaveInfo>("PlayerSaveData", _playerSaveInfo);
    }

    public void addDeath(int count) {
        _playerSaveInfo.totalDeaths += count; 
        SaveGame();
    }

    public void AddLevelAndTime(string level, float time) {
        //if we have that level key
        if (_playerSaveInfo.levelAndTime.ContainsKey(level))
        {
            //if the new time is greater than the old time 
            if (time < _playerSaveInfo.levelAndTime[level])
            {
                //replace old time with new time 
                _playerSaveInfo.levelAndTime[level] = time;
                SaveGame();
            }
        }
        //else we don't have that level key
        else {
            _playerSaveInfo.levelAndTime.Add(level, time);
            SaveGame();
        }
      
    }

    private string formatTime(float time) {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int miliseconds = Mathf.FloorToInt(time * 100);
        miliseconds = miliseconds % 100;
        string formattedTime = string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, miliseconds); //change time format to minutes:seconds:miliseconds
        string output = formattedTime;
        return output;
    }

}
