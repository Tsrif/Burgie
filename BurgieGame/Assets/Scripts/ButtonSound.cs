using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {

    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    private Button button { get { return GetComponent<Button>(); } }
    // Use this for initialization
    void Start () {
        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;

        button.onClick.AddListener(() => PlaySound());
        
	}

    void PlaySound() {
        source.PlayOneShot(sound);
    }
}
