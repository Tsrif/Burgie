using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArea : MonoBehaviour {

    //Get current camera and new camera
    //get current location and new location
    //disable current camera and enable new camera
    //move character to new location

    //public GameObject camera1;
    //public GameObject camera2;

    public Transform newPos;
   

        //I'll fix this later
    //public Vector3 secondPos = new Vector3(8.5f,-14.1f,0f);
    //public Vector3 firstPos = new Vector3(84.55f, -39.77f, 0f);

    public Vector3 currentPos;

    public GameObject Player;

    public GameObject to;
    public GameObject from;

    private void Start()
    {
        currentPos = newPos.position;
       // camera1.GetComponent<Camera>().enabled = true;
       // camera2.GetComponent<Camera>().enabled = false;
    }

    void swapLocAndCam() {

        Vector3 temp = new Vector3(newPos.position.x, newPos.position.y, newPos.position.z);
        Player.transform.position = temp;

        to.SetActive(false);
        from.SetActive(true);

       // camera1.GetComponent<Camera>().enabled = !camera1.GetComponent<Camera>().enabled;
       // camera2.GetComponent<Camera>().enabled = !camera2.GetComponent<Camera>().enabled;


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            swapLocAndCam();
        }

    }

   
}

