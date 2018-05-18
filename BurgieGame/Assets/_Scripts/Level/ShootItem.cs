using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootItem : MonoBehaviour {
    //create a bottle
    //shoot it out
    //wait
    //repeat

    public GameObject itemPrefab;
    public float xForce;
    public float yForce;
    public float power;
    public float waitTime;
    public float aliveTime;

    //Vector3 position = new Vector3(UnityEngine.Random.Range(-5, 5f), UnityEngine.Random.Range(-5, 5f), 0);
    //SpriteController circle = Instantiate(circlePrefab, position, Quaternion.identity);

    // Use this for initialization
    void Start () {
        StartCoroutine(wait(waitTime, aliveTime));
    }

    IEnumerator wait(float waitTime, float aliveTime)
    {
        while (true) {
            //wait
            yield return new WaitForSeconds(waitTime);
            //create a bottle
            Vector3 position = this.transform.position;
            GameObject newBottle = Instantiate(itemPrefab, position, Quaternion.identity);
            //shoot bottle
            newBottle.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce * power, yForce* power));
            //wait
            yield return new WaitForSeconds(aliveTime);
            //destroy the bottle
            Destroy(newBottle);
            //shootBottle();
        }
    }

    
}
