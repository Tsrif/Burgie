using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMusic : MonoBehaviour {

    void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Music");
        Destroy(obj);
    }
}
