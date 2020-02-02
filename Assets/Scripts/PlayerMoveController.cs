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

  private bool isWallSliding = false;
  public float wallSlideSpeed;

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
    ApplyMovement();
    // CheckJump();
    // CheckWallSlide();
    // ApplyWallSlide();
  }

  private void FixedUpdate(){
    CheckMoveInput();
    CheckMovementDirection();
  }

  private void CheckJump(){
    if(Input.GetKeyDown(KeyCode.Space)){
      Debug.Log("JUMPING");
      //if the player is on the ground and the user is pressing the space key
      //the user can jump
      if (isGrounded == true)
      {
          isJumping = true;
          jumpTimeCounter = jumpTime;
          rb.velocity = Vector2.up * jumpForce;
      }
      //wall Jump rules
      else if(isWallSliding){
        Debug.Log("WALL JUMP");
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity = Vector2.up * jumpForce;
      }
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

  private void ApplyMovement(){

    CheckJump();
    CheckWallSlide();

    //rules for ground movement
    if(isGrounded){

    }
    //rules for wall movement
    else if(isWallSliding){
      //go no faster than the wallSlideSpeed
      if(rb.velocity.y < -wallSlideSpeed){
        rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
      }
      //TODO: break from wall
    }
    //rules for air movement
    else{

    }
  }

  private void CheckWallSlide(){
    // Debug.Log(isTouchingLowerWall);
    // Debug.Log(!isGrounded);
    // Debug.Log(rb.velocity.y);
    if(isTouchingLowerWall && !isGrounded && rb.velocity.y < 0){
      Debug.Log("WALL SLIDING");
      isWallSliding = true;
    }
    else{
      isWallSliding = false;
    }
  }

  private void ApplyWallSlide(){
    if(isWallSliding && rb.velocity.y < -wallSlideSpeed){
      rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
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
      //if(!Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.Space))
      moveInputDirection = Input.GetAxisRaw("Horizontal");
      if (Input.GetKey(KeyCode.E))
      {
          moveInputDirection = 0;
      }
      else
      {
          moveInputDirection = Input.GetAxisRaw("Horizontal");
      }
      if(
      //you are trying to move right but you wall sliding into a wall on the right
      (moveInputDirection == 1 &&  isFacingRight && isWallSliding) ||
      //you are trying to move left but you wall sliding into a wall on the left
      (moveInputDirection == -1 && !isFacingRight && isWallSliding)
      ){
        //don't do anything
      }
      else{
        rb.position = (rb.position + new Vector2(moveInputDirection * speed * Time.deltaTime, 0));
      }

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
