using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCamera : MonoBehaviour
{
    public GameObject to;
    public GameObject from;

    public void SwapCameras()
    {
        to.SetActive(false);
        from.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SwapCameras();
        }
    }
}
