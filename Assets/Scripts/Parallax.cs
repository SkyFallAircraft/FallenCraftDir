using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    
    public Transform[] backgrounds; //array of back and forgrounds to be parallaxed
    private float[] parallaxScales;  // the proportion of the camera's movement to move the backgrounds
    public float smoothing = 1;  // how smooth the parallax is going to be.  make sure to set this above 0

    private Transform cam; // reference to the main camera's transform
    private Vector3 previousCamPosition; // the position of the camera in the previous frame

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        previousCamPosition = cam.position;

        // assigning coresponding parallax scales
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement becaus tthe previous frame multiplied by the scale
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            // set a target x position which is the current position plus the parallax multiplied
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previousCamPos to the camera's position at the end of the frame

        previousCamPosition = cam.position;

    }
}
