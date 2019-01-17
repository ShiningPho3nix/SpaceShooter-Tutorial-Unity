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
        Debug.Log(tag.ToString() + " " + other.tag.ToString());
        if (other.tag == "Boundary")
        {
            return;
        }
        if (tag == "Player" && other.tag == "Asteroid")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            playerController.DamageTaken(asteroidDamage);
        }
        if (tag == "Player" &&other.tag == "Enemy")
        {
            playerController.DamageTaken(enemyHitDamage);
            Instantiate(enemyExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
        if (tag == "Player" && other.tag == "EnemyShot")
        {
            playerController.DamageTaken(enemyShotDamage);
            Destroy(other.gameObject);
        }
        if (tag == "PlayerShot" &&  other.tag == "Asteroid")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue); //TODO asteroid score value?
            gameController.DecreaseHazardCount(); //asteroid count?
        }
        if (tag == "PlayerShot" && other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(enemyExplosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue); //TODO enemy score value?
            gameController.DecreaseHazardCount(); //TODO enemy count?
        }
    }

}
