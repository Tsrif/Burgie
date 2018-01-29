using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    private void Awake()
    {
        //On loading a scene the object with the music attached will be created
        //if you go to a new scene then come back you'll have two of the same object
        //so, destroy the newest one
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Music");
        if (objects.Length >1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
