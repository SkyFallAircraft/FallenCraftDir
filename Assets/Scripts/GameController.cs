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

    //Game Pause
    public bool gamePaused;

    /// <summary>
    /// Pauses the game
    /// </summary>
    /// <param name="p">Is the game paused?</param>
    public void OnPaused(bool p)
    {
        gamePaused = p;
    }
}
