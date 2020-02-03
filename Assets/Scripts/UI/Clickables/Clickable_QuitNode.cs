using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable_QuitNode : Clickable
{
    GameManager gMan;

    private void Start()
    {
        gMan = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public Color highlightColor;

    public override void OnClick()
    {
        gMan.GetComponent<AudioLibrary>().Misc(misc.Button, 1);
        Application.Quit();
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
