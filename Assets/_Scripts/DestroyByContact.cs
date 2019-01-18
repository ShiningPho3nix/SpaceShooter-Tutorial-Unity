using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject enemyExplosion;
    public int scoreValue;

    public float asteroidDamage;
    public float enemyHitDamage;
    public float enemyShotDamage;

    private GameController gameController;
    private PlayerController playerController;
    private readonly PlayerController enemyController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        GameObject playerControllerObject = GameObject.FindGameObjectWithTag("Player");
        GameObject enemyControllerObject = GameObject.FindGameObjectWithTag("Enemy"); // TODO initiate enermycontroller, as soon as the script exists
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
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            playerController.DamageTaken(asteroidDamage);
        }
        if (CompareTag("Enemy") && other.CompareTag("Player"))
        {
            playerController.DamageTaken(enemyHitDamage);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Instantiate(enemyExplosion, transform.position, transform.rotation);
        }
        if (CompareTag("EnemyShot") && other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            playerController.DamageTaken(enemyShotDamage);
            Destroy(gameObject);
        }
        if (CompareTag("Asteroid") &&  other.CompareTag("PlayerShot"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue);
            gameController.DecreaseHazardCount(); //asteroid count?
        }
        if (CompareTag("Enemy") && other.CompareTag("PlayerShot"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(enemyExplosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue); 
            //gameController.DecreaseHazardCount(); //TODO enemy count?
        }
        if (CompareTag("Enemy") && other.CompareTag("Asteroid"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(enemyExplosion, transform.position, transform.rotation);
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            //enemy.damagetaken?
        }
    }
}
