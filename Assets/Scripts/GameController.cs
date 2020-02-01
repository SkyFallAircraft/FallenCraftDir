using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance; //global instance
    public static GameController Main //public accessor for global instance
    {
        get
        {
            return instance;
        }
    }

    //Editor References
    public Pause pause;
    public Clickable_SceneNode deathSceneTransition;

    //Editor + Runtime values
    [NonSerialized]
    public bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPaused(bool p)
    {
        gamePaused = p;
    }
}
