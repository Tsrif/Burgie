using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevelCompletion : MonoBehaviour
{
    public List<Button> buttons;
    public SaveData sd;
    // Use this for initialization
    void Start()
    {
        int sdCount = sd._playerSaveInfo.levelAndTime.Count;
        //Make the buttons for the levels we haven't beaten yet not iteractable 
        for (int i = 0; i < sdCount; i++)
        {
            buttons[i].interactable = true;
        }
    }
}
