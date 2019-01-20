using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidValues : MonoBehaviour
{
    public float asteroidLife;
    public float asteroidDamage;
    public float asteroidArmor;

    public void SetAsteroidLife(float life)
    {
        asteroidLife = life;
    }

    public float GetAsteroidLife()
    {
        return asteroidLife;
    }

    public void DamageTaken(float damage)
    {
        float realDamageTaken = damage - asteroidArmor;
        if (realDamageTaken < 1)
            realDamageTaken = 1;
        asteroidLife = asteroidLife - realDamageTaken;
    }
}

