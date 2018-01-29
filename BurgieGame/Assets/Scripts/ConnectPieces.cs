using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPieces : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Burger Pieces")
        {
            //check if a hinge exists beffore making another one 
            if (gameObject.GetComponent<HingeJoint2D>() != null)
            {
                return;
            }
            else
            {
                //create a hinge and attach it to the piece 
                HingeJoint2D hinge = gameObject.AddComponent<HingeJoint2D>() as HingeJoint2D;
                //connect the hinge to what we collided with 
                hinge.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            }

        }
    }

    
}
