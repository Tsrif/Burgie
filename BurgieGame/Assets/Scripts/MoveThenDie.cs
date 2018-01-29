using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThenDie : MonoBehaviour {
    private Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    public float speed;
    public float delay;

    private void Start()
    {
        StartCoroutine(killSelf());
    }
    private void FixedUpdate()
    {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y); //move to the right
    }

    public IEnumerator killSelf()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
       
}
