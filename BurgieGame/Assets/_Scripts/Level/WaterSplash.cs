using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterSplash : MonoBehaviour {

    public GameObject splash;
    public AudioClip clip;
    private AudioSource source;
    public static event Action drownPlayer;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Vector3 position = collision.transform.position;
            collision.gameObject.SetActive(false); //deactivate player 
            GameObject ps = Instantiate(splash, position, Quaternion.Euler(new Vector3(-90, 0, 0))); //create splash particle system
            if (drownPlayer!= null) {
                drownPlayer();
            }
            source.PlayOneShot(clip);

        }
    }
}
