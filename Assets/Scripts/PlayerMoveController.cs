using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveController : MonoBehaviour {

  private Rigidbody2D rb;

  public float speed;
  private float moveInputDirection;
  private bool isFacingRight = true;

  private bool isJumping;
  public float jumpForce;
  private float jumpTimeCounter;
  public float jumpTime;

  public float leapPressure;
  public float minWallLeap;
  public float maxWallLeap;

  public Transform lowerWallCheck;

  public LayerMask whatIsWall;
  private bool isTouchingLowerWall;

  public float wallCheckDistance;

  private bool isGrounded;
  public Transform groundCheck;
  public float groundCheckRadius;
  public LayerMask whatIsGround;


  private bool doOnce = false;

  void Start(){
    rb = GetComponent<Rigidbody2D>();
    isGrounded = true;

    leapPressure = 0f;
  }

  void Update(){
    CheckSurroundings();
    CheckJump();
  }

  private void FixedUpdate(){
    CheckMoveInput();
    CheckMovementDirection();
  }

  private void CheckJump(){
    //if the player is on the ground and the user is pressing the space key
    //the user can jump
    if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
    {
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity = Vector2.up * jumpForce;
    }
    //if the user is pressing the jump key but the user is already jumping
    if (Input.GetKey(KeyCode.Space) && isJumping == true)
    {
        //if there is time left in the jump
        if (jumpTimeCounter > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
    }
    //the user has stopped pressing the jump key
    if (Input.GetKeyUp(KeyCode.Space))
    {
        isJumping = false;
    }
  }




  //TODO : update for 3/4 assets?
  private void Flip(){
      isFacingRight = !isFacingRight;
      transform.Rotate(0.0f, 180.0f, 0.0f);
      //use this to rotate character upside down when on a ceiling. 180 first.
  }

  private void CheckSurroundings(){
    if(isTouchingLowerWall){
      Debug.Log("Touching Wall");
    }


    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    isTouchingLowerWall = Physics2D.Raycast(lowerWallCheck.position, transform.right, wallCheckDistance, whatIsWall);
  }

  private void OnDrawGizmos(){
    Gizmos.DrawLine(lowerWallCheck.position, new Vector3(lowerWallCheck.position.x + wallCheckDistance, lowerWallCheck.position.y, lowerWallCheck.position.z));
    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

  }

  private void CheckMoveInput()
  {
      if(!Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.Space))
      moveInputDirection = Input.GetAxisRaw("Horizontal");
      if (Input.GetKey(KeyCode.E))
      {
          moveInputDirection = 0;
      }
      else
      {
          moveInputDirection = Input.GetAxisRaw("Horizontal");
      }

      rb.position = (rb.position + new Vector2(moveInputDirection * speed * Time.deltaTime, 0));
  }

  private void CheckMovementDirection()
  {
      if (isFacingRight && moveInputDirection < 0)
      {
          Flip();
      }
      else if (!isFacingRight && moveInputDirection > 0)
      {
          Flip();
      }
  }
}
