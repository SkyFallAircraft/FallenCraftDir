using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gMan;
    public static AudioLibrary adLib;
    Clickable_SceneNode clickableScene;
    public GameObject pauseFilterPrefab;
    Scene[] scenes;

    //GameObjects
    //public Clickable_SliderNode VolumeSlider;

    //SpriteRenderers
    public SpriteRenderer fadeInOut;

    //Integers
    int countLoaded;

    bool check;
    public bool loaded = false;
    double transparent = 1;


    void Awake()
    {
        adLib = this.GetComponent<AudioLibrary>();
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

    private void Start()
    {
        if (SceneManager.GetSceneByName("GliderLevel").isLoaded)
        {
            //adLib.MusicSounds(music.Win, true, 1f, true);
            adLib.MusicSounds(music.Wind, true, 1f, true);
        }
    }

    public void Update()
    {
        Debug.Log(SceneManager.GetSceneByName("GliderLevel").isLoaded);
        if (loaded)
        {
            transparent -= 0.8f * Time.unscaledDeltaTime;
            //Debug.Log(transparent);
            fadeInOut.color = new Color(0f, 0f, 0f, (float)transparent);
            //Debug.Log(fadeInOut.color.a);
            if (transparent <= 0)
            {
                var pauseScript = gameObject.GetComponent<Pause>();
                var pauseFilter = Instantiate(pauseFilterPrefab);
                var mainCam = Camera.main.gameObject;
                pauseScript.pauseFilter = pauseFilter;
                pauseFilter.transform.parent = Camera.main.gameObject.transform;
                pauseFilter.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, -5);
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
