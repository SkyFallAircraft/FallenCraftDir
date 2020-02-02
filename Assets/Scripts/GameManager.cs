using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gMan;
    Clickable_SceneNode clickableScene;

    //GameObjects
    //public Clickable_SliderNode VolumeSlider;

    //SpriteRenderers
    public SpriteRenderer fadeInOut;

    //Scene management
    public Scene currentScene;

    //Integers
    int countLoaded;

    bool check;
    public bool loaded = false;
    double transparent = 1;

    void Awake()
    {
        if (gMan == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            //SceneManager.activeSceneChanged += ChangedActiveScene;
            //check = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (loaded)
        {
            transparent -= 0.8f * Time.unscaledDeltaTime;
            //Debug.Log(transparent);
            fadeInOut.color = new Color(0f, 0f, 0f, (float)transparent);
            //Debug.Log(fadeInOut.color.a);
            if (transparent <= 0)
            {
                Debug.Log("loaded");
                transparent = 1;
                loaded = false;
            }
        }
        /*if (SceneManager.sceneCount < 2)
        {
            VolumeSlider = FindObjectOfType<Clickable_SliderNode>();
        }
        if (VolumeSlider != null) volume = VolumeSlider.output;
        gMan.audioLibrary.musicPlayer.volume = volume;*/
    }
}
