using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rope Swing gives Burgie the ability to swing from a rope
//I decided to make a separate script for handling swinging from rope in case I decided it was an ability or something that Burgie learned
public class RopeSwing : MonoBehaviour {

    //if we touch the bulb on the rope
    //We can only unattach from the rope if we jump
    public float swingForce;

    private BurgerController player;
	void Start () {
        player = gameObject.GetComponent<BurgerController>();
    }
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Horizontal"); // get movement direction 
        switch (player._playerState) {
            case PlayerState.ON_ROPE:
                player.GetComponent<Animator>().SetInteger("AnimState", (int)player._playerState); //set animation to idle
                player.resetCharacter();
                //if you move then you are swinging
                if (move != 0) { player._playerState = PlayerState.SWINGING; }
                //if you press space you're off the rope
                if (Input.GetButtonDown("Jump")) { player._playerState = PlayerState.OFF_ROPE; }
                break;

            case PlayerState.SWINGING:
                player.GetComponent<Animator>().SetInteger("AnimState", (int)player._playerState); //set to walking animation 
                //move the player by swingForce instead of speed
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(move * swingForce, player.GetComponent<Rigidbody2D>().velocity.y);
                player.Flip(move); //flip character
                //if space bar is pressed then change state to jumping
                if (Input.GetButtonDown("Jump")) { player._playerState = PlayerState.OFF_ROPE; }
                //else set state to on_rope 
                else if (move == 0) { player._playerState = PlayerState.ON_ROPE; }
                break;

            case PlayerState.ROPE_JUMP:
                player.GetComponent<Animator>().SetInteger("AnimState", (int)player._playerState);
                if (player.facingRight)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2((1) * player.walljumpForce, (float)player.wallJumpHeight)); //pushed off a wall - right
                }
                else
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2((-1) * player.walljumpForce, (float)player.wallJumpHeight)); //pushed off a wall - left
                }
                //set state to in_air 
                player._playerState = PlayerState.IN_AIR;
                break;

            case PlayerState.OFF_ROPE:
                player.GetComponent<Animator>().SetInteger("AnimState", (int)player._playerState);
                //Remove attachment to rope
                Destroy(gameObject.GetComponent<HingeJoint2D>());
                //set state to rope_jump 
                player._playerState = PlayerState.ROPE_JUMP; 
                break;

            default:
                break;
        }
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bulb")
        {
            //check if a hinge exists beffore making another one 
            if (gameObject.GetComponent<HingeJoint2D>() != null)
            {
                player._playerState = PlayerState.ON_ROPE;
            }
            else
            {
                //create a hinge and attach it to the player 
                HingeJoint2D hinge = gameObject.AddComponent<HingeJoint2D>() as HingeJoint2D;
                //connect the hinge to what we collided with 
                hinge.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
                player._playerState = PlayerState.ON_ROPE;
            }

        }

    }
}
