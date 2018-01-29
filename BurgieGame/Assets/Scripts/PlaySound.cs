using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
    //private AudioSource source { get { return GetComponent<AudioSource>(); } }
    private AudioSource source;
    private bool winner;

    private void OnEnable()
    {
        EndOfLevel.winner += Winner;
    }

    private void OnDisable()
    {
        EndOfLevel.winner += Winner;
    }

    void Winner() {
        winner = true;
       
    }
    public void playSound(AudioClip clip)
    {
        if (gameObject.GetComponent<AudioSource>() == true && !winner) {
            source = gameObject.GetComponent<AudioSource>();
            source.Stop();
            source.PlayOneShot(clip);
        }
    }

  
}
