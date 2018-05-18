using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LettersOverPlayer : MonoBehaviour {
    public float speed;
    private float startY;



    private void Awake()
    {
        startY = transform.position.y;
    }

    private void Update()
    {
        transform.position =
            Vector2.Lerp(new Vector2(transform.position.x, startY), new Vector2(transform.position.x, startY + 2), Mathf.PingPong((Mathf.Sin(speed * Time.time)), 1));
    }


}
