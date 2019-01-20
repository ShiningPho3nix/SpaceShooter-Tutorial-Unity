using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Asteroids
{
    public GameObject asteroidExplosion;
    public float asteroidDamage;
    public float asteroidLife;
}

[System.Serializable]
public class Enemies
{
    public GameObject enemyExplosion;

    public float enemyHitDamage;
    public float enemyShotDamage;
    public float enemyLife;
}

[System.Serializable]
public class PlayerValues
{
    public GameObject playerExplosion;
    public float playerShotDamage;
}

public class DestroyByContact : MonoBehaviour
{
    public int scoreValue;
    
    private GameController gameController;
    private PlayerController playerController;

    public Asteroids asteroids;
    public Enemies enemies;
    public PlayerValues playerValues;

    public AsteroidValues asteroidValues;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        GameObject playerControllerObject = GameObject.FindGameObjectWithTag("Player");
        // Same for later classes Enemy and Asteroid, to let them take damage and deal damage variable to each other and the player. Also life and other things

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Could not find GameController Object!");
        }
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.Log("Could not find PlayerController Object!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
        if (CompareTag("Asteroid") && other.CompareTag("Player"))
        {
            asteroidValues = GameObject.Find(name).GetComponent<AsteroidValues>();
            asteroidValues.DamageTaken(5);
            Debug.Log(asteroidValues.GetAsteroidLife());
            if (asteroidValues.GetAsteroidLife() <= 0f)
            {
                Instantiate(asteroids.asteroidExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            playerController.DamageTaken(asteroidValues.asteroidDamage);
            if (gameController.GetPlayerLife() == 0f)
            {
                Instantiate(playerValues.playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
        }
        if (CompareTag("Enemy") && other.CompareTag("Player"))
        {
            Instantiate(enemies.enemyExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
            playerController.DamageTaken(enemies.enemyHitDamage);
            if (gameController.GetPlayerLife() == 0f)
            {
                Instantiate(playerValues.playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
        }
        if (CompareTag("EnemyShot") && other.CompareTag("Player"))
        {
            playerController.DamageTaken(enemies.enemyShotDamage);
            Destroy(gameObject);
            if (gameController.GetPlayerLife() == 0f)
            {
                Instantiate(playerValues.playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
        }
        if (CompareTag("Asteroid") && other.CompareTag("PlayerShot"))
        {
            Instantiate(asteroids.asteroidExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.AddScore(scoreValue);
            gameController.DecreaseHazardCount(); //asteroid count?
        }
        if (CompareTag("Enemy") && other.CompareTag("PlayerShot"))
        {
            Instantiate(enemies.enemyExplosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.AddScore(scoreValue); 
            //gameController.DecreaseHazardCount(); //TODO enemy count?
        }
        if (CompareTag("Enemy") && other.CompareTag("Asteroid"))
        {
            Instantiate(enemies.enemyExplosion, transform.position, transform.rotation);
            Instantiate(asteroids.asteroidExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
            //enemy.damagetaken?
        }
    }
}
