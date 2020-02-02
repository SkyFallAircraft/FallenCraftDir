using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    Rigidbody2D cloudParticle;
    public double timeLeftToSwitch = 10;
    double speed = 1;
    float cloudSpeed = 1f;
    int check = 0;

    // Start is called before the first frame update
    void Start()
    {
        cloudParticle = this.GetComponent<Rigidbody2D>();
        cloudParticle.AddForce(new Vector2(1, 0));
        Debug.Log(cloudParticle.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        /*timeLeftToSwitch -= Time.deltaTime * speed;
        if (timeLeftToSwitch <= 0)
        {
            Debug.Log("switching");
            Debug.Log(cloudParticle.velocity);
            if (cloudParticle.startSpeed >= cloudSpeed)
            {
                Debug.Log("New Direction");
                check = 1;
            }
            else if (cloudParticle.startSpeed <= -cloudSpeed)
            {
                Debug.Log("New Direction");
                check = 2;
            }

            if (check == 1)
            {
                cloudParticle.AddForce(-Time.deltaTime * 3);
                if (cloudParticle.velocity <= -cloudSpeed)
                {
                    timeLeftToSwitch = 15;
                    check = 0;
                }
            }
            else if (check == 2)
            {
                cloudParticle.startSpeed += Time.deltaTime * 3;
                if (cloudParticle.startSpeed >= cloudSpeed)
                {
                    timeLeftToSwitch = 15;
                    check = 0;
                }
            }
        }*/
    }
}
