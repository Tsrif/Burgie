using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkController : MonoBehaviour
{
    public float speed;
    public bool facingRight;

    public bool walled;
    public Transform wallCheck;
    public float wallRadius = 0.2f;
    public LayerMask whatIsWall;

    public bool grounded;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private Animator animator;
    private Rigidbody2D rb;
   // private float lowestY;
    //public float time;
    public float jumpForce;
    private bool paused;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        paused = false;
    }


    // Use this for initialization
    void Start()
    {
       
        //lowestY = transform.position.y; //kinda specific to one map at the moment, but this is to prevent the fork from falling beneath the map
        StartCoroutine(waitThenJump());
        StartCoroutine(randomlyChangeSpeed());

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
       
        //if (transform.position.y <= lowestY) {
        //    transform.position = new Vector3(transform.position.x,lowestY, transform.position.z);
        //}

        //Draws a circle in front of fork and checks whether the circle is hitting a "wall"
        walled = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
        animator.SetBool("Walled", walled);

        //Draws a circle below fork and checks whether the circle is touching the ground 
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        animator.SetBool("Grounded", grounded);
        animator.SetInteger("AnimState",0);


        if (!facingRight)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y); //move to the left
        }
        else if (facingRight)
        {
            rb.velocity = new Vector2(1 * speed, rb.velocity.y); //move to the right
        }


    }

    //at random times randomly change the speed
    private IEnumerator randomlyChangeSpeed() {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 3f));
            speed = Random.Range(4f, 10f);
        }
    }

    private IEnumerator waitThenJump() {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 1f));
            if (grounded == true) {
                jump();
            }
        }
    }

    private void jump() {
        if (paused)
        {
            return;
        }
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "WallColliders")
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
