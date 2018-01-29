using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour {
    public static Action killPlayer;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private void OnEnable()
    {
        EndOfLevel.winner += stopSounds;
        
    }

    private void OnDisable()
    {
        EndOfLevel.winner -= stopSounds;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if (killPlayer != null) {
                killPlayer();//if the player touches an enemy send out a notification for the player to die
            }
        }
    }

    private void stopSounds() {
        if (source == null) {
            return;
        }
        source.enabled = false;
    }
}
