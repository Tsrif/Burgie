using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EndOfLevel : MonoBehaviour
{
    public Canvas WinCanvas;
    public BurgerController player;
    public GameObject menuItem; //For some reason without menu item, the text on my buttons won't show up
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    public static event Action winner;
    public SaveData saveData;

    private void Start()
    {
        saveData = SaveData._instance;
        menuItem.SetActive(false);
        //sound = gameObject.GetComponent<AudioSource>();
        source.playOnAwake = false;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            menuItem.SetActive(true);
            if (winner != null)
            {
                winner(); //send winner notification
                source.Play();
            }
        }
    }
}
