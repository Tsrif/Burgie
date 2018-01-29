using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateLetters : MonoBehaviour {

    public float speed;
    private float startY;
    public float newY;


    private void Awake()
    {
        startY = transform.position.y;
    }
   
    private void Update()
    {
        transform.position = 
            Vector2.Lerp(new Vector2(transform.position.x, startY), new Vector2(transform.position.x, newY), Mathf.PingPong((Mathf.Sin(speed * Time.time)), 1));
    }

   
}
