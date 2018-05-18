using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapController : MonoBehaviour
{
    public float speed;
    public float distance = 1f;
    private float horizontal;

    public bool facingRight;

    public bool walled;
    public Transform wallCheck;
    float wallRadius = 0.2f;
    public LayerMask whatIsWall;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Use this for initialization
    void Start()
    {
        facingRight = false;
    }


    private void FixedUpdate()
    {

        //Draws a circle in front of Toastie and checks whether the circle is hitting a "wall"
        walled = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
        animator.SetBool("Walled", walled);

        if (!facingRight)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y); //move to the left
        }
        else if (facingRight)
        {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y); //move to the right
        }

        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (walled)
        {
            if (facingRight)
            {
                Flip(-1);
            }

            else if (!facingRight)
            {
                Flip(1);
            }



        }
    }

    //flips the character to opposite direction they are facing 
    public void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            //Vector3 theScale = transform.localScale;

            //theScale.x *= -1;
            //transform.localScale = theScale;
        }
    }



}
