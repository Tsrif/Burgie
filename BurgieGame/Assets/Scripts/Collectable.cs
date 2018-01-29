using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Collectable : MonoBehaviour {

    public GameObject text;
    public GameObject ps;
    public float time;
    private bool run = true;
    
	// Use this for initialization
	void Start () {
        text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            text.SetActive(true);
            StartCoroutine(fadeAway());
        }
    }

    private IEnumerator fadeAway() {
        while (run) {
            yield return new WaitForSeconds(time);
            Color newColor = gameObject.GetComponent<SpriteRenderer>().color;
            newColor.a = newColor.a - 0.2f;
            gameObject.GetComponent<SpriteRenderer>().color = newColor;
            text.GetComponent<TextMesh>().color = newColor;
            if (newColor.a <= 0) {
                run = false;
                ps.GetComponent<ParticleSystem>().Clear();
                ps.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}
