using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This Class controlls the game, it spwans asteroids, enemies later. It contains the game loop, allows to get a score, increases the dificulty
/// and lets the player restart the game if the player is game over
/// </summary>
public class GameController : MonoBehaviour
{
    public GameObject[] asteroidHazards;
    public GameObject[] enemyHazards;
    public Vector3 spawnValues;

    private float playerLife;
    public float spawnRate;
    public float waveRate;
    public float firstSpawn;

    private int waveCount;
    private int asteroidHazardWaveCount;
    private int enemyHazardWaveCount;
    private int score;
    public int asteroidHazardCount;
    public int enemyHazardCount;

    public Text playerLifeText;
    public Text restartText;
    public Text gameOverText;
    public Text scoreText;
    public Text waveText;
    public Text hazardText;

    private bool gameOver;
    private bool restart;

    private void Start()
    {
        waveCount = 1;
        score = 0;
        asteroidHazardWaveCount = asteroidHazardCount;
        gameOverText.text = "";
        restartText.text = "";
        UpdateText();
        StartCoroutine(spawnWaves());
    }

    /// <summary>
    /// This Method Instantiate the public GameObject hazard at a Random Position with a Random Rotation via Quaternion.
    /// </summary>
    private IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(firstSpawn);
        while (true)
        {
            for (int i = 0; i < asteroidHazardCount; i++)
            {
                if (gameOver)
                {
                    break;
                }
                GameObject asteroidHazard = asteroidHazards[Random.Range(0, asteroidHazards.Length)];
                Debug.Log(asteroidHazard.name);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(asteroidHazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnRate);
            }
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
            UpdateWaves();
            UpdateText();
            yield return new WaitForSeconds(waveRate);
        }
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateText();
    }

    public void UpdateText()
    {
        scoreText.text = "Score: " + score;
        waveText.text = "Wave: " + waveCount;
        hazardText.text = "Remaining: " + asteroidHazardWaveCount;
        playerLifeText.text = "Life: " + playerLife;
    }

    private void UpdateWaves()
    {
        waveCount++;
        float waveRateDecrease = waveRate * 0.02f;
        waveRate = waveRate - waveRateDecrease;
        if (waveRate < 0.1f)
        {
            waveRate = 0.1f;
        }
        asteroidHazardCount += waveCount;
        spawnRate = spawnRate - (spawnRate * 0.05f);
        if (spawnRate < 0.01f)
        {
            spawnRate = 0.01f;
        }
        asteroidHazardWaveCount = asteroidHazardCount + asteroidHazardWaveCount; // Next Round the Hazards are the hazards for the wave + the remaining hazards, if there are any.
    }

    public void DecreaseHazardCount()
    {
        asteroidHazardWaveCount--;
        UpdateText();
    }

    public void SetPlayerLife(float newLife)
    {
        if (newLife < 0f)
        {
            playerLife = 0f;
            GameOver();
        }
        else
        {
            playerLife = newLife;
        }
        UpdateText();
    }

    public float GetPlayerLife()
    {
        return playerLife;
    }

    public void GameOver()
    {
        gameOverText.text = "GAME OVER!";
        gameOver = true;
    }
}
