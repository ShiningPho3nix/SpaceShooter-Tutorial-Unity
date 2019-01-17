using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public float minMass;
    public float maxMass;

    private Rigidbody rb;
    private float randomMovementX;
    private float randomMovementZ;
    private float asteroidSpeed;
    private float asteroidMass;
    private Vector3 asteroidMovement;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        randomMovementX = Random.Range(-0.5f, 0.5f);
        randomMovementZ = Random.Range(-0.5f, -1.0f);
        asteroidMovement = new Vector3(randomMovementX, 0.0f, randomMovementZ);

        asteroidSpeed = Random.Range(minSpeed, maxSpeed);
        asteroidMass = Random.Range(minMass, maxMass);

        rb.mass = asteroidMass;
        rb.velocity = asteroidMovement * asteroidSpeed;
    }

    /// <summary>
    /// This Method will prevend that the asteroids leave the boundery via y coordinate.
    /// </summary>
    private void FixedUpdate() => rb.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
}
