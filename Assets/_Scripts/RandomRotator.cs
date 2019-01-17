using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class adds a Random Rotation to an Object with a Rigidbody attached.
/// </summary>
public class RandomRotator : MonoBehaviour
{
    public float maxTumble;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * Random.Range(0.0f, maxTumble);
    }
}
