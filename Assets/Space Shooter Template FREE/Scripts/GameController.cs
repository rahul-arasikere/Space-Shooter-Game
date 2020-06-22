using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{
    public Canvas startCanvas;
    public Canvas pauseCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public Player player;
    public LevelController levelController;
    public HighScoreController highScoreController;
    public GameObject highScoreText;
    public Text scoreText;
    private long score;
    // Start is called before the first frame update
    void Start()
    {
        ResetUI();
        ActivateGame(false);
        startCanvas.gameObject.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();
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
        ActivateGame(false);
        gameOverCanvas.gameObject.SetActive(true);
        if (highScoreController.AddHighScore(score))
        {
            highScoreText.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.gameObject.SetActive(false);
        }
    }

    public void ActivateGame(bool value)
    {
        player.gameObject.SetActive(value);
        levelController.gameObject.SetActive(value);
        ResetUI();
        gameCanvas.gameObject.SetActive(value);
        Time.timeScale = (value) ? 1 : 0;
    }

    public void IncrementScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
