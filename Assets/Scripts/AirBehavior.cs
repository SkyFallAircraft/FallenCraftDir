using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBehavior : MonoBehaviour
{

    ParticleSystem airParticle;
    public double timeLeftToSwitch = 10;
    double speed = 1;
    double airSpeed;
    int check = 0;

    // Start is called before the first frame update
    void Start()
    {
        airParticle = this.GetComponent<ParticleSystem>();
        airSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeftToSwitch -= Time.deltaTime * speed;
        if (this.name == "AirSystem")
        {
            if (timeLeftToSwitch <= 0)
            {
                if (airParticle.startSpeed >= airSpeed)
                {
                    check = 1;
                }
                else if (airParticle.startSpeed <= -airSpeed)
                {
                    check = 2;
                }

                if (check == 1)
                {
                    airParticle.startSpeed -= Time.deltaTime * 3;
                    if (airParticle.startSpeed <= -airSpeed)
                    {
                        timeLeftToSwitch = 15;
                        check = 0;
                    }
                }
                else if (check == 2)
                {
                    airParticle.startSpeed += Time.deltaTime * 3;
                    if (airParticle.startSpeed >= airSpeed)
                    {
                        timeLeftToSwitch = 15;
                        check = 0;
                    }
                }
            }
        }
    }
}
