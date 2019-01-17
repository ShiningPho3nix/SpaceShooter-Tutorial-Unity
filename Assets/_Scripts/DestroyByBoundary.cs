using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Could not find GameController Object!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        if (other.tag == "Asteroid")
        {
            gameController.DecreaseHazardCount();
        }
    }
}
