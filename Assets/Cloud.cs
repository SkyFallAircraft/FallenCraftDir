using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform cloudTransform;
    private bool isTraveling = false;
    private bool acceptingInput = true;
    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    public float cloudSpeed;
    public GameObject player;

    
        
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        cloudTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (acceptingInput == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                acceptingInput = false;
                isTraveling = true;
                //rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                if (isTraveling)
                {
                    Debug.Log("isTraveling");
                    isMovingLeft = true;
                    
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                acceptingInput = false;
                isTraveling = true;
                if (isTraveling)
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                {
                    isMovingRight = true;
                    Debug.Log("isTraveling");
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                acceptingInput = false;
                //doMovement
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                isTraveling = true;
                if (isTraveling)
                {
                    isMovingDown = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                acceptingInput = false;
                isTraveling = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                if (isTraveling)
                {
                    isMovingUp = true;
                }
            }
        }
        if (isMovingRight)
        {
            transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
        }
        if (isMovingLeft)
        {
            transform.Translate(Vector3.right * -cloudSpeed * Time.deltaTime);
        }
        if (isMovingUp)
        {
            transform.Translate(Vector3.up * cloudSpeed * Time.deltaTime);
        }
        if (isMovingDown)
        {
            transform.Translate(Vector3.up * -cloudSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (isTraveling == true && collision.gameObject.tag != "Player")
        {
            isTraveling = false;
            rb.velocity = new Vector2(0, 0);
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isTraveling == false)
        {
            acceptingInput = true;
        }



        if (collision.gameObject.tag == "Player")
        {

            player.transform.SetParent(cloudTransform, true);
            
            
        }
        if (player.GetComponent<TestScript>().isJumping == true)
        {
            player.transform.SetParent(null);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            acceptingInput = false;
        }
    }
}
