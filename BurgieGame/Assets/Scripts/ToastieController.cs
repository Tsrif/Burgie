using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastieController : MonoBehaviour
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
    private bool paused;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        paused = false;
    }

    private void OnEnable()
    {
        GameController.currentGameState += Pause;
    }
    private void OnDisable()
    {
        GameController.currentGameState -= Pause;
    }

    private void FixedUpdate()
    {
        if (paused) {
            return;
        }
       
        //Draws a circle in front of Toastie and checks whether the circle is hitting a "wall"
        walled = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
        animator.SetBool("Walled", walled);

        if (!facingRight) {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y); //move to the left
        }
        else if (facingRight) {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y); //move to the right
        }


    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "WallColliders")
        {
            if (facingRight) {
                Flip(-1);
            }

            else if (!facingRight)
            {
                Flip(1);
            }
        }
    }

    public void Pause(GameState gameState)
    {
        if (gameState == GameState.MENU)
        {
            animator.SetInteger("AnimState", 20);
            paused = true;
            rb.Sleep();
        }
        else { paused = false; rb.WakeUp(); animator.SetInteger("AnimState", 0); }
    }

    //flips the character to opposite direction they are facing 
    public void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

   
}
