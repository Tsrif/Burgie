using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {
    public Animator animator;
    public string levelToLoad;

    //Fades to the next given level
    public void FadeToLevel(string levelName) {
        //Set the levelToLoad to the given levelName
        levelToLoad = levelName;
        //Trigger the fadeOut animation
        animator.SetTrigger("FadeOut");
    }

    public void FadeToTitle()
    {
        //Set the levelToLoad to "Title_Screen"
        levelToLoad = "Title_Screen";
        //Trigger the fadeOut animation
        animator.SetTrigger("FadeOut");
    }

    //Reloads the current level
    public void FadeRestart()
    {
        //Set the levelToLoad to the name of the active scene
        levelToLoad = SceneManager.GetActiveScene().name;
        //Trigger the fadeOut animation
        animator.SetTrigger("FadeOut");
    }

    //After FadeOut is completed, the animator will call this event
    public void OnFadeComplete() {
        SceneManager.LoadScene(levelToLoad);
    }

    
}
