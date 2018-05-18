using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpText : MonoBehaviour {
    private MeshRenderer text;

	// Use this for initialization
	void Start () {
        text = GetComponent<MeshRenderer>();
        text.enabled = false;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            text.enabled = true;
        }
        
    }
}
