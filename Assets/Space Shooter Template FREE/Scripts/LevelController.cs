using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Serializable classes
[System.Serializable]
public class EnemyWaves
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;

    public EnemyWaves(float time, GameObject w)
    {
        timeToStart = time;
        wave = w;
    }
}

#endregion

public class LevelController : MonoBehaviour
{

    //Serializable classes implements
    List<EnemyWaves> enemyWaves = new List<EnemyWaves>();
    public int currentLevel = 1;
    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();
    public GameObject[] waves;
    public float wavesToSpawn = 5;
    public float timeBetweenWaves = 8;

    public GameObject LevelCompleteCanvas;
    public Text levelDisplay;
    Camera mainCamera;
    public void LevelGenerator()
    {
        enemyWaves = new List<EnemyWaves>();
        int time = 0;
        for (int i = 0; i < wavesToSpawn; i++, time += 8)
        {
            enemyWaves.Add(new EnemyWaves(time, waves[Random.Range(0, waves.Length)]));
        }
    }

    public void LevelComplete()
    {
        wavesToSpawn *= 1.25f;
        timeBetweenWaves *= 0.95f;
        timeForNewPowerup *= 0.85f;
    }

    private void OnEnable()
    {
        mainCamera = Camera.main;
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
        LevelStart();
    }
    private void LevelStart()
    {
        int i;
        LevelGenerator();
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        for (i = 0; i < enemyWaves.Count; i++)
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }
        StartCoroutine(LevelWinCheck(enemyWaves[i].timeToStart + 8));
    }

    IEnumerator LevelWinCheck(float delay)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        currentLevel += 1;
        levelDisplay.text = "Level " + currentLevel + " Starting";
        LevelCompleteCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        LevelCompleteCanvas.SetActive(false);
        LevelComplete();
        LevelStart();
    }

    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
            Instantiate(Wave);
    }

    //endless coroutine generating 'levelUp' bonuses. 
    IEnumerator PowerupBonusCreation()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerUp,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2),
                Quaternion.identity
                );
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
