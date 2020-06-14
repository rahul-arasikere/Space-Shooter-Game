using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public int maxLives = 3;
    private int _currentLife;

    public void start()
    {
        _currentLife = maxLives;
    }
    public void PlayerKilled()
    {
        _currentLife--;
        if (_currentLife == 0)
        {
            GameController.Instance.GameOver();
        }
        else
        {
            Player.instance.Respawn();
        }
    }
}
