using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { IDLE, WALKING, JUMPING, PARACHUTE, ON_WALL, DANCING, FREEFALLING, DOUBLEJUMP, IN_AIR, WALL_JUMP, ON_ROPE,SWINGING,ROPE_JUMP,OFF_ROPE};

public class BurgerController: MonoBehaviour
{
    [FoldoutGroup("Movement")]
    public float speed;
    [FoldoutGroup("Movement")]
    public float jumpForce;
    [FoldoutGroup("Movement")]
    public float doubleJumpForce;
    [FoldoutGroup("Movement")]
    public float walljumpForce;
    [FoldoutGroup("Movement")]
    public float wallJumpHeight;
    [FoldoutGroup("Movement")]
    public float distance = 1f;
    [FoldoutGroup("Movement")]
    private float horizontal;
    [FoldoutGroup("Movement")]
    public float maxVelocityY;
    [FoldoutGroup("Movement")]
    public Vector3 originalPosition;

    [FoldoutGroup("Bools")]
    public bool facingRight;
    [FoldoutGroup("Bools")]
    public bool doubleJump;
    [FoldoutGroup("Bools")]
    public bool freeFall;
    [FoldoutGroup("Bools")]
    public bool dancing;
    [FoldoutGroup("Bools")]
    public bool grounded;
    [FoldoutGroup("Bools")]
    public bool walled;


    [FoldoutGroup("Refs")]
    public Transform groundCheck;
    [FoldoutGroup("Refs")]
    public Transform wallCheck;
    [FoldoutGroup("Refs")]
    public GameObject parachute;
    [FoldoutGroup("Refs")]
    public GameObject confetti;
    [FoldoutGroup("Refs")]
    public List<AudioClip> sounds = new List<AudioClip>();

    [FoldoutGroup("Checks")]
    public float groundRadius = 0.2f;
    [FoldoutGroup("Checks")]
    public LayerMask whatIsGround;
    [FoldoutGroup("Checks")]
    public float wallRadius = 0.2f;
    [FoldoutGroup("Checks")]
    public LayerMask whatIsWall;
    [FoldoutGroup("Checks")]
    public Vector3 boxSizeWall;
    [FoldoutGroup("Checks")]
    public Vector3 boxSizeGround;

    [TitleGroup("Player State")]
    // [EnumToggleButtons]
    public PlayerState _playerState;

    /*PRIVATE VARIABLES */
    private float move;
    private bool jumpInput;
    private bool parachuteInput;
    private bool paused;

    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    private Animator animator;
    private Rigidbody2D rb;
    private float mass;
    private float drag;

   
    private void Awake()
    {
        confetti.SetActive(false);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mass = rb.mass;
        drag = rb.drag;
        _playerState = PlayerState.IDLE;
        paused = false;
        
    }

    private void OnEnable()
    {
        EndOfLevel.winner += Dance; //Subscribe to event
        GameController.currentGameState += Pause;

    }
    private void OnDisable()
    {
        EndOfLevel.winner -= Dance; //Unsubscribe to event
        GameController.currentGameState -= Pause;
    }

    void Start()
    {
        originalPosition = this.transform.position;
        facingRight = true;
        freeFall = false;
    }


    private void Update()
    {
        //check horizontal
        move = Input.GetAxis("Horizontal"); // get movement direction 
        //check jump
        if (Input.GetButtonDown("Jump"))
        {
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }
        //check parachute
        if (Input.GetButtonDown("Parachute"))
        {
            parachuteInput = true;
        }
        else
        {
            parachuteInput = false;
        }
    }

    private void FixedUpdate()
    {
        //Don't do anything if we are dancing or paused
        if (dancing || paused  )
        {
            return;
        }

        //Controls the highest a player can go from jumping/doubleJumping/wallJumping
        if (rb.velocity.y >= maxVelocityY)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityY);
        }

        ControlPlayer();        
    }
   

    void ControlPlayer() {
        //Draws a circle below Burgie and checks whether the circle is hitting the "ground"
        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        grounded = Physics2D.OverlapBox(groundCheck.position, boxSizeGround, 0, whatIsGround);
        animator.SetBool("Grounded", grounded); //set in animator that we are grounded
        //Draws a circle in front of Burgie and checks whether the circle is hitting a "wall"
        //walled = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
        walled = Physics2D.OverlapBox(wallCheck.position, boxSizeWall, 0, whatIsWall);
        animator.SetBool("Walled", walled); //set in animator that we are walled
        switch (_playerState)
        {
            case PlayerState.IDLE:
                animator.SetInteger("AnimState", (int)_playerState);
                //if we press arrow keys set to walking
                if (move != 0) { _playerState = PlayerState.WALKING; }
                //if we press space bar set to jumping 
                if (jumpInput) { _playerState = PlayerState.JUMPING; }
                //if we aren't on the ground or on a wall then we are in the air
                if (!grounded && !walled) { _playerState = PlayerState.IN_AIR; }
                break;

            case PlayerState.WALKING:
                animator.SetInteger("AnimState", (int)_playerState);
                Flip(move); //flip character
                rb.velocity = new Vector2(move * speed, rb.velocity.y); //move left or right
                //if space bar is pressed then change state to jumping
                if (jumpInput) { _playerState = PlayerState.JUMPING; }
                //if we aren't on the ground or on a wall then we are in the air
                else if (!grounded && !walled) { _playerState = PlayerState.IN_AIR; }
                //else set state to idle 
                else if (move == 0) { _playerState = PlayerState.IDLE; }
                break;

            case PlayerState.JUMPING:
                animator.SetInteger("AnimState", (int)_playerState);
                //apply jump force to character
                rb.AddForce(new Vector2(move, jumpForce), ForceMode2D.Impulse);
                //set state to in air
                _playerState = PlayerState.IN_AIR;
                break;

            case PlayerState.PARACHUTE:
                if (!parachute.activeSelf) {
                    animator.SetInteger("AnimState", (int)_playerState);
                    parachute.SetActive(true);
                    //lower mass and increase drag to float down slower
                    rb.mass /= 2;
                    rb.drag = 10;
                }
                else if (parachute.activeSelf)
                {
                    parachute.SetActive(false);
                    rb.mass = mass;
                    rb.drag = drag;
                }
                //set state to in air
                _playerState = PlayerState.IN_AIR;
                break;

            case PlayerState.ON_WALL:
                animator.SetInteger("AnimState", (int)_playerState);
                resetCharacter();
                //animator.SetInteger("AnimState", (int)_playerState); //set animation to on wall
                //if space bar is pressed set state to wall jump
                if (jumpInput) { _playerState = PlayerState.WALL_JUMP; }
                //if grounded set state to walking
                else if (grounded) { source.PlayOneShot(sounds[0]); resetCharacter(); _playerState = PlayerState.WALKING; }
                break;

            case PlayerState.DANCING:
                print("We are dancing ");
                break;

            case PlayerState.FREEFALLING:
                animator.SetInteger("AnimState", (int)_playerState);
                //if space bar is pressed set state to double jump
                if (jumpInput) { _playerState = PlayerState.DOUBLEJUMP; }
                //if grounded set state to walking
                if (grounded) { resetCharacter(); _playerState = PlayerState.WALKING; }
                break;

            case PlayerState.DOUBLEJUMP:
                animator.SetInteger("AnimState", (int)_playerState);
                if (doubleJump == false)
                {
                    //apply jump force
                    rb.AddForce(new Vector2(move, doubleJumpForce), ForceMode2D.Impulse);
                    //we used our double jump
                    doubleJump = true;
                    //set state to in air
                    _playerState = PlayerState.IN_AIR;
                }
                break;

            case PlayerState.IN_AIR:
                animator.SetInteger("AnimState", (int)_playerState);
                Flip(move); //flip character if needed
                rb.velocity = new Vector2(move * speed, rb.velocity.y); //move left or right
                //if we jump check to see if doublejump is available, if doublejump is available double jump
                if (jumpInput)
                {
                    if (!doubleJump)
                    {
                        _playerState = PlayerState.DOUBLEJUMP;
                    }
                }

                //if we press the parachute button then deploy parachute
                if (parachuteInput){_playerState = PlayerState.PARACHUTE;}
                //if we become grounded set state to on ground 
                if (grounded) { resetCharacter(); source.PlayOneShot(sounds[0]); _playerState = PlayerState.IDLE; }//reset character, play ground impact sound, set to idle
                //if we become walled set state to on wall                                                                                              
                if (walled) { source.PlayOneShot(sounds[1]); _playerState = PlayerState.ON_WALL; }
                break;

            case PlayerState.WALL_JUMP:
                animator.SetInteger("AnimState", (int)_playerState);
                //technically the character is facing right but it's backwards when on a wall because of the animation
                if (!facingRight) {
                    rb.AddForce(new Vector2((1) * walljumpForce, (float)wallJumpHeight)); //pushed off a wall - right
                }
                else {
                    rb.AddForce(new Vector2((-1) * walljumpForce, (float)wallJumpHeight)); //pushed off a wall - left
                }
                //set state to free falling 
                _playerState = PlayerState.FREEFALLING;
                break;

            default:
                break;
        }
    }
    
    
    
    //Changes animation to sliding down wall
    private void OnCollisionEnter2D(Collision2D col)
    {
        resetCharacter(); //touching anything should reset you - hoping to fix issue where you can get locked on objects after freefalling
                          //possible other fix is to create a timer that takes you out of freefall after x seconds. 
        if (col.gameObject.tag == "WallColliders") {

            animator.SetInteger("AnimState", (int)_playerState); //set to wall hang 
        }
    }

    //turns the player around after done touching the wall because the animation for being on the wall is flipped 
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "WallColliders")
        {
            Flip(-transform.localScale.x);
            animator.SetInteger("AnimState", (int)_playerState);
        }
    }

    public void Dance() {
        dancing = true;
        confetti.SetActive(true);
        resetCharacter();
        print("Winner Winner Win");
        _playerState = PlayerState.DANCING;
        animator.SetInteger("AnimState", (int)_playerState);
    }

    //when we are paused take away inputs from player 
    //then sleep the rigidbody to freeze position
    //when playing give back inputs and wake up the body 
    private void Pause(GameState gameState) {
        if (gameState == GameState.MENU)
        {
            animator.SetInteger("AnimState", 20);
            paused = true;
            rb.Sleep();
        }
        else { paused = false; rb.WakeUp(); }
    }

    //reset certain values
    public void resetCharacter() {
        doubleJump = false;
        parachute.SetActive(false);
        freeFall = false;
        rb.mass = mass;
        rb.drag = drag;
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

    //Used to draw the groundCheck and wallCheck overlap circles 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        Gizmos.DrawWireCube(groundCheck.position, boxSizeGround);
        //Gizmos.DrawWireSphere(wallCheck.position, wallRadius);
        Gizmos.DrawWireCube(wallCheck.position, boxSizeWall);
    }
}
