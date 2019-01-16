using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        float zMovement = Random.Range(0.0f, -1.0f);
        float xMovement = Random.Range(-1.0f, 1.0f);
        Vector3 movement = new Vector3(xMovement, 0.0f, zMovement) * Random.Range(minSpeed, maxSpeed);
        rb.velocity = movement;
    }
}
