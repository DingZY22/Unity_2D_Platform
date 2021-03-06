using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float douleJumpSpeed;
    public float climbSpeed;

    public bool faceRight;
    private bool canDoubleJump;
    private bool isLadder;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private BoxCollider2D myFeet;

    private bool isGround;
    private bool isOneWayPlatform;
    private enum playerState { onGround, onOneWayPlatform, onMovingPlatform }
    [SerializeField]private playerState state;
    
    private bool isJumping;
    private bool isDoubleJumping;
    private bool isFalling;
    private bool isDoubleFalling;
    private bool isClimbing;

    public  float restoreTime;
    private float playerGravity;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidBody.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.PLAYALIVE)
        {
            Jump();
            Climb();
            Flip();
            Run();
            checkGrounded();
            oneWayPlatform();
            checkLadder();
            SwitchAnimation();
            CheckAirStatus();

        }
    }

    void Flip()
    {
        float moveDir = Input.GetAxis("Horizontal");
        if ((moveDir < 0 && faceRight) || (moveDir > 0 && !faceRight))
        {
            faceRight = !faceRight;
            this.gameObject.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    void Run()
    {

        this.myRigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, this.myRigidBody.velocity.y);
        bool playerHasXaxisSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Run", playerHasXaxisSpeed);

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                myAnimator.SetBool("Jump", true);
                Vector2 jumpVec = new Vector2(0.0f, jumpSpeed);
                myRigidBody.velocity = Vector2.up * jumpVec;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    myAnimator.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVec = new Vector2(0.0f, douleJumpSpeed);
                    myRigidBody.velocity = Vector2.up * doubleJumpVec;
                    canDoubleJump = false;
                }
            }
        }
    }

    void Climb()
    {
        //Bug: Player on top of the ladder still able to climb.
        if (isLadder)
        {
            myAnimator.SetBool("ClimbingStay", true);
            myRigidBody.gravityScale = 0.0f;
            if (isClimbing)
            {
                myAnimator.SetBool("Climbing", true);
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxis("Vertical") * this.climbSpeed);
            }
            else
            {
                //From other actions to the middle of the ladder
                if (isJumping || isFalling || isDoubleFalling || isDoubleJumping)
                {
                    
                    myAnimator.SetBool("Climbing", false);
                    myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, Input.GetAxis("Vertical") * this.climbSpeed);
                }
                //On top of bottom of the ladder
                else
                {
                    myAnimator.SetBool("Climbing", false);
                    myAnimator.SetBool("ClimbingStay", false);
                    myRigidBody.velocity= new Vector2(myRigidBody.velocity.x, 0.0f);
                }
            }
        }
        else 
        {           
            myAnimator.SetBool("Climbing", false);
            myAnimator.SetBool("ClimbingStay", false);
            myAnimator.SetBool("Fall", true);
            myRigidBody.gravityScale = playerGravity;
        }    
    }

    void SwitchAnimation()
    {
        myAnimator.SetBool("Idle", false);
        if (myAnimator.GetBool("Jump"))
        {
            if (myRigidBody.velocity.y < 0.0f)
            {
                myAnimator.SetBool("Jump", false);
                myAnimator.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnimator.SetBool("Fall", false);
            myAnimator.SetBool("Idle", true);
        }

        if (myAnimator.GetBool("DoubleJump"))
        {
            if (myRigidBody.velocity.y < 0.0f)
            {
                myAnimator.SetBool("DoubleJump", false);
                myAnimator.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnimator.SetBool("DoubleFall", false);
            myAnimator.SetBool("Idle", true);
        }

    }
    void checkGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
                   myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        
        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));

    }

    void checkLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    void oneWayPlatform()
    {
        if (isGround && this.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Player");
        }
        if (isOneWayPlatform && Input.GetAxis("Vertical") < -0.1f)
        {
            this.gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("restorePlayerLayer", restoreTime);
        }
       
    }

    void restorePlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    
    }

    void CheckAirStatus()
    {
        isJumping = myAnimator.GetBool("Jump");
        isDoubleJumping = myAnimator.GetBool("DoubleJump");
        isFalling = myAnimator.GetBool("Fall");
        isDoubleFalling = myAnimator.GetBool("DoubleFall");
        isClimbing = Input.GetAxis("Vertical")>0.8f || Input.GetAxis("Vertical")<-0.8f;

    }


}
