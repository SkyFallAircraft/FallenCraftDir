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
    public GameObject[] pauseButtons;
    GameManager gMan;
    bool check = false;

    private void Start()
    {
        gameController = GetComponent<GameController>();
        pauseFilter = GameObject.FindWithTag("PauseFilter");
        paused = false;
        PauseGame(paused);
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            paused = !paused;
            PauseGame(paused);
        }
        if(SceneManager.sceneCount < 2)
        {
            pauseFilter = GameObject.FindWithTag("pauseFilter");
            if (pauseFilter) pauseFilter.SetActive(false);
        }
        if (gMan.loaded == false && check == false)
        {
            pauseFilter.SetActive(false);
            check = false;
        }
    }

    public void PauseGame(bool p)
    {
        if (p)
        {
            Time.timeScale = 0f;
            if (pauseFilter != null)
            {
                pauseFilter.SetActive(true);
            }
            else
            {
                pauseFilter = GameObject.FindWithTag("PauseFilter");
                pauseFilter.SetActive(true);
            }
            paused = true;
            gameController.OnPaused(paused);
            for (int i = 0; i < pauseButtons.Length; i++) pauseButtons[i].SetActive(true);
        }
        
        else
        {
            Time.timeScale = 1f;
            pauseFilter.SetActive(false);
            paused = false;
            gameController.OnPaused(paused);
            for (int i = 0; i < pauseButtons.Length; i++) pauseButtons[i].SetActive(false);
        }
    }
}
