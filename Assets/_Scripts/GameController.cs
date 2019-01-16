using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public float spawnRate;
    private float nextSpawn = 0.0f;

    private void Start()
    {
        if (Time.time > nextSpawn)
        {
            Debug.Log("If Schleife erreicht " + Time.time + " " + nextSpawn + " " + spawnRate);
            nextSpawn = Time.time + spawnRate;
            spawnWaves();
        }
    }

    private void spawnWaves()
    {
        Debug.Log("SpawnWaves erreicht");
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0.0f, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Debug.Log(spawnPosition + " " + spawnRotation);
        Instantiate(hazard, spawnPosition, spawnRotation);
    }
}
