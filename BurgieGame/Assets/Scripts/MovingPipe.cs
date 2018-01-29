using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPipe : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed;
    public bool showPath;

    public float fraction = 0;



    // Use this for initialization
    void Start()
    {
        startPoint = this.transform.position;
        //speed = 0;
    }

    private void Update()
    {
        //Mathf.PingPong
        //fraction = Mathf.Clamp((Mathf.Sin(speed * Time.time)), 0, 1);
        transform.position = Vector2.Lerp(startPoint, endPoint, Mathf.PingPong((Mathf.Sin(speed * Time.time)),1));
        //print(Mathf.Clamp((Mathf.Sin(speed * Time.time)), 0,1));
        

    }

    void swapPoints() {
          Vector3 temp = startPoint;
          startPoint = endPoint;
          endPoint = temp;
    }

    void shouldMovePipe(float s) {
        //speed = s;
    }
    private void OnDrawGizmos()
    {
        if (showPath) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPoint, endPoint);
        }
      
    }
}
