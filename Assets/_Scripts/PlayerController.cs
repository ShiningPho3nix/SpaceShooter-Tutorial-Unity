using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private GameController gameController;
    private AudioSource shotAudio;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotspawn;

    private float nextShot = 0.0f;
    public float speed;
    public float tilt;
    public float life;
    public float fireRate;
    public float armor;

    private void Start()
    {
        shotAudio = gameObject.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Could not find GameController Object!");
        }
        gameController.SetPlayerLife(life);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
            (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
            shotAudio.Play();
        }
        if (gameController.GetPlayerLife() <= 0f)
        {
            gameController.GameOver();
        }
    }

    public void DamageTaken(float damage)
    {
        life = life - (damage - armor);
        gameController.SetPlayerLife(life);
    }

    public void SetLife(float newLife)
    {
        life = newLife;
    }
}
