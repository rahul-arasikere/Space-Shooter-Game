using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    public int maxLives = 3;
    private int _currentLife;
    public Text LiveText;

    public void Start()
    {
        _currentLife = maxLives;
        LiveText.text = _currentLife.ToString();

    }
    public void PlayerKilled()
    {
        _currentLife -= 1;
        if (_currentLife == 0)
        {
            GameController.Instance.GameOver();
        }
        else
        {
            LiveText.text = _currentLife.ToString();
            StartCoroutine(DoRespawnCoroutine());
        }
    }

    private IEnumerator DoRespawnCoroutine()
    {
        Player.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        Player.instance.gameObject.SetActive(true);
        Player.instance.Respawn();
    }
}
