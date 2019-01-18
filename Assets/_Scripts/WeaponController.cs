using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float MaxFireRate;
    public float delay;

    private AudioSource enemyShotAudio;

    // Start is called before the first frame update
    void Start()
    {
        float fireRate = Random.Range(1f, MaxFireRate);
        enemyShotAudio = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
    }

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        enemyShotAudio.Play();
    }
}
