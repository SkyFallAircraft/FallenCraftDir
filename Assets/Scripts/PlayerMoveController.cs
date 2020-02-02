using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveController : MonoBehaviour {

  private Rigidbody2D rb;

  public float speed;
  private float moveInputDirection;
  private bool isFacingRight = true;
  private Vector2 startLocation = new Vector2(-3, 1);

  //boundaries of map
  public float LOWER_BOUND_X = -100;
  public float UPPER_BOUND_X = 75;
  public float LOWER_BOUND_Y = -50;
  public float UPPER_BOUND_Y = 55;

  public Animator animator;

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

  private bool isGliding = false;
  public float glideSpeedX;
  public float glideSpeedY;

  private bool canDoubleJump = false;
  private bool doubleJumpAvailable = false;
  public float doubleJumpSpeed;

  private bool hasDoubleJump = false;
  private bool hasGlide = false;
  private bool hasHookShot = false;

  public GameManager gMan;
  public AudioLibrary adLib;

  void Start(){
        gMan = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        adLib = gMan.GetComponent<AudioLibrary>();
        rb = GetComponent<Rigidbody2D>();
    isGrounded = true;

    leapPressure = 0f;
    //gMan = GameObject.findWithTag("GameManager").GetComponent<GameManager>();
    //gMan.GetComponent<AudioLibrary>().Player(playerEffect.[sound effect name], [volume]);
  }

  void Update(){
    CheckSurroundings();
    ApplyMovement();
    // CheckJump();
    // CheckWallSlide();
    // ApplyWallSlide();
    if(isJumping== true)
        {
            animator.SetBool("IsInAir", true);
        }
  }

  private void FixedUpdate(){
    CheckMoveInput();
    if(!isGliding){
      CheckMovementDirection();
    }

  }

  private void Launch()
 {
    Debug.Log("LANCH");
     leapPressure = Mathf.Clamp(leapPressure, minWallLeap, maxWallLeap);
     Vector2 push = new Vector2((transform.right.x * -leapPressure), (transform.up.y * leapPressure));

     rb.AddForce(push, ForceMode2D.Impulse);

     leapPressure = 0f;
     Flip();

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
        // isJumping = true;
        // jumpTimeCounter = jumpTime;
        Launch();
        //rb.velocity = Vector2.up * jumpForce;
      }
      //double Jump
      //need power up, legal state, and charged Jump
      else if( hasDoubleJump && canDoubleJump && doubleJumpAvailable){
        rb.velocity = Vector2.up * doubleJumpSpeed;
        doubleJumpAvailable = false;
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
    // //rules for wall movement
    // else if(isWallSliding){
    //   //go no faster than the wallSlideSpeed
    //   if(rb.velocity.y < -wallSlideSpeed){
    //     rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    //   }
    //
    // }
    //rules for air movement
    else{

    }
  }

  private void CheckWallSlide(){
    // Debug.Log(isTouchingLowerWall);
    // Debug.Log(!isGrounded);
    // Debug.Log(rb.velocity.y);
    if(isTouchingLowerWall && !isGrounded && rb.velocity.y < 0){
      //Debug.Log("WALL SLIDING");
      isWallSliding = true;
    }
    else{
      isWallSliding = false;
            animator.SetBool("IsWallSliding", false);
        }
  }

  private void ApplyWallSlide(){
    if(isWallSliding && rb.velocity.y < -wallSlideSpeed){
            //Debug.Log("Bobby is nice");
      rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
    }
  }

  private void Flip(){
      isFacingRight = !isFacingRight;
      transform.Rotate(0.0f, 180.0f, 0.0f);
      //use this to rotate character upside down when on a ceiling. 180 first.
  }

  private void KillPlayer(){
    adLib.Player(playerEffect.Dead, 1);
    //move player back to Start
    rb.position = startLocation;
    //zero out velocity
    rb.velocity = new Vector2(0,0);
    //remove powerups
    hasGlide = false;
    hasHookShot = false;
    hasDoubleJump = false;
  }

  private void CheckSurroundings(){
    if(isTouchingLowerWall){
      Debug.Log("Touching Wall");
    }


    isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    isTouchingLowerWall = Physics2D.Raycast(lowerWallCheck.position, transform.right, wallCheckDistance, whatIsWall);

    if(isGrounded || isTouchingLowerWall){
      isGliding = false;
      animator.SetBool("IsGliding", false);
      //reset double jump when touching a wall or the ground
      doubleJumpAvailable = true;
      if (isGrounded == true)
      {
          animator.SetBool("IsInAir", false);
      }
    }
    if(!isGrounded && !isWallSliding){
      //can double jump any time not touching a wall or the ground
      canDoubleJump = true;
      animator.SetBool("IsInAir", true);
    }
    //player is out of bounds
    if(
    rb.position.x < LOWER_BOUND_X ||
    rb.position.x > UPPER_BOUND_X ||
    rb.position.y < LOWER_BOUND_Y ||
    rb.position.y > UPPER_BOUND_Y
    ){
      KillPlayer();
      Debug.Log(rb.position);
    }
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
      if(Input.GetKeyDown(KeyCode.LeftShift)){
        Debug.Log("CHANGING GLIDE");
        isGliding = !isGliding;
            if(isGliding == true)
            {
                animator.SetBool("IsGliding", true);
            }
            if(isGliding == false)
            {
                animator.SetBool("IsGliding", false);
            }
      }
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
      (Input.GetKey("d") &&  isFacingRight && isWallSliding) ||
      //you are trying to move left but you wall sliding into a wall on the left
      (Input.GetKey("a") && !isFacingRight && isWallSliding)
      ){
        //Apply WallSlide movement rules
        ApplyWallSlide();
            animator.SetBool("IsWallSliding", true);
        }
      else{
            //movement while gliding
            if (isGliding){
          Debug.Log("GLIDING");
          int glideDirection = isFacingRight ? 1 : -1;
          rb.gravityScale = 0;
          rb.velocity = new Vector2( glideDirection* glideSpeedX * Time.deltaTime, -glideSpeedY * Time.deltaTime);
        }
        else{
          rb.gravityScale = 5;
          rb.position = (rb.position + new Vector2(moveInputDirection * speed * Time.deltaTime, 0));
                animator.SetFloat("Speed", Mathf.Abs(moveInputDirection * speed * Time.deltaTime));
        }
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


  void OnCollisionEnter2D(Collision2D collision){
    if(collision.gameObject.tag == "doubleJump"){
      hasDoubleJump = true;
      Destroy(collision.gameObject);
    }
    else if(collision.gameObject.tag == "glide"){
      hasGlide = true;
      Destroy(collision.gameObject);
    }
    else if(collision.gameObject.tag == "ship"){
      if(hasGlide){
        //BOAZ DO SOMETHING HERE TO ADD GLIDER TO THE SHIP
      }
      if(hasDoubleJump){
        //BOAZ DO SOMETHING HERE TO ADD THRUSTER TO SHIP
      }
      //turn off all powerups
      hasGlide = false;
      hasDoubleJump = false;
    }
    else if(collision.gameObject.tag == "spike"){
      //if you hit a spike you die
      KillPlayer();
    }

  }


}
