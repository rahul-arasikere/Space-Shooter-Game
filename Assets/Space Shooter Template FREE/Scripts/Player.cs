using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class Player : MonoBehaviour
{
    public GameObject destructionFX;

    public static Player instance;

    private Vector3 startPosition;

    public void start()
    {
        startPosition = gameObject.transform.position;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)
    {
        Destruction();
    }

    public void Respawn()
    {
        PlayerShooting.instance.weaponPower = 1;
        transform.position = startPosition;
    }
    

    public void DoRespawn()
    {
        // PlayerShooting.instance.weaponPower = 1;
        // gameObject.transform.position = startPosition;
        // gameObject.SetActive(true);
    }

    IEnumerator DoRespawnCoroutine()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        PlayerShooting.instance.weaponPower = 1;
        gameObject.transform.position = startPosition;
        gameObject.SetActive(true);
    }

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        // Destroy(gameObject);
        PlayerController.Instance.PlayerKilled();
    }
}
















