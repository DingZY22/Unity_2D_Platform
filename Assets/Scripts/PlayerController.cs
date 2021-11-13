using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float douleJumpSpeed;

    public bool faceRight;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private BoxCollider2D myFeet;

    private bool isGround;
    private bool canDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.PLAYALIVE)
        {
            Jump();
            Flip();
            Run();
            //Attack();
            checkGrounded();
            SwitchAnimation();
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
                   myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"));
        
    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            myAnimator.SetTrigger("Attack");
        }
    }

}
