using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Windows.Input;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused;
    GameController gameController;
    public GameObject pauseFilter;
    //public GameObject pauseFilterPrefab;
    public GameObject[] pauseButtons;
    GameManager gMan;
    bool check = false;

    private void Start()
    {
        gameController = GetComponent<GameController>();
        //Debug.Log(pauseFilter);
        paused = false;
        PauseGame(paused);
    }

    // Update is called once per frame
    void Update()
    {
        if(((Input.GetKeyDown(KeyCode.P) /*&& paused*/)|| (Input.GetKeyDown(KeyCode.Escape) /*&& paused*/)))
        {
            paused = !paused;
            PauseGame(paused);
        }
        /*else if(((Input.GetKeyDown(KeyCode.P) && !paused) || (Input.GetKeyDown(KeyCode.Escape) && !paused)))
        {
            paused = true;
            PauseGame(paused);
        }*/
    }

    public void PauseGame(bool p)
    {
        //pauseFilter = GameObject.FindWithTag("PauseFilter");
        Debug.Log(pauseFilter);
        if (p)
        {
            Debug.Log("PausedGame");
            Time.timeScale = 0f;
            pauseFilter.SetActive(true);
            Debug.Log("Active: " + pauseFilter.activeSelf);
            //if(pauseFilter = null) Debug.LogError("pauseFilter null");
            paused = true;
            //gameController.OnPaused(paused);
            //for (int i = 0; i < pauseButtons.Length; i++) pauseButtons[i].SetActive(true);
        }
        else
        {
            Debug.Log("UnpausedGame");
            Time.timeScale = 1f;
            pauseFilter.SetActive(false);
            Debug.Log("Not active: " + pauseFilter.activeSelf);
            paused = false;
            //gameController.OnPaused(paused);
            //for (int i = 0; i < pauseButtons.Length; i++) pauseButtons[i].SetActive(false);
        }
    }
}
