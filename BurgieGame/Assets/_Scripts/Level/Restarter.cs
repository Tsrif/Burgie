using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class Restarter : MonoBehaviour   {
        public BurgerController player;
        public Camera2DFollow MainCamera;
    public static event Action restart;
        private void Start()
        {
            player = GameObject.Find("Player").GetComponent<BurgerController>();
            MainCamera = GameObject.Find("Main Camera").GetComponent<Camera2DFollow>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
        if (other.tag == "Player")
        {
            if (restart!=null) {
                restart();
            }
           
                MainCamera.transform.position = MainCamera.originalPosition;
                player.transform.position = player.originalPosition;
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            }
        }
    }