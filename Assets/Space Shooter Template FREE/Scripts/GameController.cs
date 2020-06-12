using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public Canvas startCanvas;
    public Canvas pauseCanvas;
    public Canvas gameCanvas;
    public Player player;
    public LevelController levelController;
    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
        startCanvas.gameObject.SetActive(true);
        ActivateGame(false);
        Time.timeScale = 0;
    }

    public void ResetUI()
    {
        startCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        ResetUI();
        ActivateGame(true);
    }

    public void PauseGame(bool value)
    {
        pauseCanvas.gameObject.SetActive(value);
        if (value){
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void ActivateGame(bool value)
    {
        player.gameObject.SetActive(value);
        levelController.gameObject.SetActive(value);
        gameCanvas.gameObject.SetActive(value);
        startCanvas.gameObject.SetActive(!value);
        Time.timeScale = 1;
    }
}
