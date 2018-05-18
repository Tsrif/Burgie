using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public void SwitchToLevel_1() {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void SwitchToLevel_2()
    {
        SceneManager.LoadScene("UnderSinkLevel", LoadSceneMode.Single);
    }
    public void SwitchToLevel_3()
    {
        SceneManager.LoadScene("Level3", LoadSceneMode.Single);
    }

    public void BackToTitleScreen() {
        SceneManager.LoadScene("Title_Screen", LoadSceneMode.Single);
    }

    public void ChangeScene(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
