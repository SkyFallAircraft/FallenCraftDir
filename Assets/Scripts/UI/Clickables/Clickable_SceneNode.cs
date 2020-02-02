using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clickable_SceneNode : Clickable
{

    GameManager gMan;
    AudioLibrary adLib;

    double transparent;

    bool sceneChange;

    public string scenes;
    public Color highlightColor;
    public float fadeSpeed = 0.8f;

    public void Awake()
    {
        gMan = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        adLib = gMan.GetComponent<AudioLibrary>();
    }

    private void Update()
    {
        if (sceneChange)
        {
            transparent +=  fadeSpeed * Time.unscaledDeltaTime;
            gMan.fadeInOut.color = new Color(0f, 0f, 0f, (float)transparent);
            if (transparent >= 1)
            {
                sceneChange = false;
                SceneManager.LoadScene(scenes);
                transparent = 0;
                gMan.loaded = true;
            }
        }
    }

    public override void OnClick()
    {
        if (scenes != null)
        {
            adLib.Player(playerEffect.Dead, 1);
            sceneChange = true;
        }
        else
        {
            Debug.LogWarning("Scene undefined on " + gameObject.name);
        }
    }
    public override void OnHover()
    {
        gameObject.GetComponent<SpriteRenderer>().color = highlightColor;
    }
    public override void OnEmpty()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
