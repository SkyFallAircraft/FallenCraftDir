using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gMan;
    //Clickable_SceneNode clickableScene;

    //GameObjects
    //public Clickable_SliderNode VolumeSlider;

    //SpriteRenderers
    public SpriteRenderer fadeInOut;

    //Scene management
    public Scene currentScene;

    void Awake()
    {
        if (gMan == null)
        {
            gMan = this;
            DontDestroyOnLoad(transform.gameObject);
            //SceneManager.activeSceneChanged += ChangedActiveScene;
            //check = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
