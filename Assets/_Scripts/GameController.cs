using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Class controlls the game, mainly spawns enemies atm.
/// </summary>
public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public float spawnRate;
    public float waveRate;
    public float firstSpawn;
    public int hazardCount;
    private int waveCount;
    private int hazardWaveCount;

    public Text scoreText;
    public Text waveText;
    public Text hazardText;
    private int score;

    private void Start()
    {
        waveCount = 1;
        score = 0;
        hazardWaveCount = hazardCount;
        UpdateText();
        StartCoroutine(spawnWaves());
    }

    /// <summary>
    /// This Method Instantiate the public GameObject hazard at a Random Position with a Random Rotation via Quaternion.
    /// </summary>
    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(firstSpawn);
        while (true)
        {
            for(int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0.0f, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnRate);
            }
            UpdateWaves();
            UpdateText();
            yield return new WaitForSeconds(waveRate);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = "Score: " + score;
        waveText.text = "Wave: " + waveCount;
        hazardText.text = "Remaining: " + hazardWaveCount;
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
        hazardCount += waveCount;
        spawnRate = spawnRate - (spawnRate * 0.05f);
        if (spawnRate < 0.01f)
        {
            spawnRate = 0.01f;
        }
        hazardWaveCount = hazardCount + hazardWaveCount; // Next Round the Hazards are the hazards for the wave + the remaining hazards, if there are any.
    }

    public void DecreaseHazardCount()
    {
        hazardWaveCount--;
        UpdateText();
    }
}
