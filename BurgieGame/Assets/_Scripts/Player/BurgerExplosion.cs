using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerExplosion : MonoBehaviour {
   // public float xForce; //Can use these to set what specific direction the pieces will fly
   // public float yForce;
    public float power;
    public List<GameObject> pieces = new List<GameObject>();
    private AudioSource source { get { return GetComponent<AudioSource>(); } }
    public AudioClip explosion;

    public List<GameObject> letters = new List<GameObject>();

    /*All the components to turn off*/
    private SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }
    private BurgerController bc { get { return GetComponent<BurgerController>(); } }
    private Animator anim { get { return GetComponent<Animator>(); } }
    private PolygonCollider2D polyColl { get { return GetComponent<PolygonCollider2D>(); } }
    private Rigidbody2D rb { get { return GetComponent<Rigidbody2D>(); } }
    int recieved = 0;

    private void OnEnable()
    {
        Enemy.killPlayer += explode;
    }

    private void OnDisable()
    {
        Enemy.killPlayer -= explode;
    }

    //Create the pieces at attached gameObject position 
    //Shoot them out in random directions
    //Disable certain components of the gameobject
    void explode() {

        //we only need to recieve one notification to explode
        recieved++;
        if (recieved == 1) {
            for (int i = 0; i < pieces.Count; i++)
            {
                Vector3 position = this.transform.position;//get the position of burgie
                GameObject piece = Instantiate(pieces[i], position, Quaternion.identity); //create a piece of the burger 
                piece.transform.parent = gameObject.transform;
                piece.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0f, 10f) * power, Random.Range(0f, 10f) * power));// send piece flying in random direction
            }
            anim.enabled = false; //turn off the animator
            bc.parachute.SetActive(false); //turn off the parachute
            bc.enabled = false; //turn off the burger controller
            sr.enabled = false; //turn off the sprite render  
            polyColl.enabled = false; // turn off the collider
            source.Stop(); //stop all current sounds
            source.PlayOneShot(explosion);//play the explosion
            rb.constraints = RigidbodyConstraints2D.FreezeAll; //freeze rigidbody position so it doesn't fall through the map
            turnOnLetters();
           
        }       
    }

    void turnOnLetters() {
        for (int i = 0; i < letters.Count; i ++) {
            Vector3 position = this.transform.position;//get the position of burgie
            GameObject letter = Instantiate(letters[i], position, Quaternion.identity); 
           // letter.transform.parent = gameObject.transform;
        }
    }
}
