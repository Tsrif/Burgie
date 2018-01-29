using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBurger : MonoBehaviour {

    public List<GameObject> pieces = new List<GameObject>();
    public float speed;
    public float power;
    public float xForce;
    public float yForce;
    private int piecesDropped;
    public GameObject fullBurg;
    
    void Start () {
        piecesDropped = 0;
        StartCoroutine(dropThenWait());
	}

    //Spawn a burger that moves
    //also kill all the game objects children(the pieces)
    void spawnMovingBurger() {
        Vector3 position = this.transform.GetChild(0).transform.position;//get the position of a child
        GameObject burger = Instantiate(fullBurg, position, Quaternion.identity); //create burger 
        burger.AddComponent<MoveThenDie>();
        burger.GetComponent<MoveThenDie>().speed = 10;
        burger.GetComponent<MoveThenDie>().delay = 5;
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        piecesDropped = 0;
    }

    //Drop a piece then wait 
    public IEnumerator dropThenWait() {
        while (true) {
            dropPiece();
            yield return new WaitForSeconds(1.0f);
            piecesDropped++;
            if (piecesDropped == 6) { spawnMovingBurger(); }
        }
    }

    //create a piece
    //then drop it 
    void dropPiece() {
        if (piecesDropped < 6) {
            Vector3 position = this.transform.position;//get the position of parent
            GameObject piece = Instantiate(pieces[piecesDropped], position, Quaternion.identity); //create a piece of the burger 
            piece.transform.parent = gameObject.transform; //attach to parent
            piece.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce * power, yForce * power)); //drop piece
        }
    }

   
}
