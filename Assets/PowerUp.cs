using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float slowDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Play"))
        {
            foreach (CarMovement car in FindObjectsOfType<CarMovement>()) 
            { 
                Destroy(car.gameObject);
            }
            CarSpawner spawner = FindObjectOfType<CarSpawner>();
            if (spawner != null)
            {
                spawner.SlowTraffic(slowDuration);
            }
            Destroy(gameObject);
        }
    }
}
