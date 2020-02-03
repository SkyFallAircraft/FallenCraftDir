using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookshot : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform topSpawnPosition;
    public Transform rightSpawnPosition;
    public Transform leftSpawnPosition;
    public bool isUp = false;
    public bool isRight = false;
    public bool isLeft = false;
    public float hookSpeed;
    private bool isMovingUp = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
