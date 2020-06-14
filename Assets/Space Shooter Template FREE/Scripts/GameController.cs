using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public Canvas startCanvas;
    public Canvas pauseCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public Player player;
    public LevelController levelController;
    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
        startCanvas.gameObject.SetActive(true);
        ActivateGame(false);
    }

    public void ResetUI()
    {
        startCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        ResetUI();
        ActivateGame(true);
    }

    public void PauseGame(bool value)
    {
        pauseCanvas.gameObject.SetActive(value);
        Time.timeScale = (value) ? 0 : 1;
    }

    public void GameOver()
    {
        gameOverCanvas.gameObject.SetActive(true);
        ActivateGame(false);
    }

    public void ActivateGame(bool value)
    {
        player.gameObject.SetActive(value);
        levelController.gameObject.SetActive(value);
        gameCanvas.gameObject.SetActive(value);
        startCanvas.gameObject.SetActive(!value);
        Time.timeScale = (value) ? 1 : 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
