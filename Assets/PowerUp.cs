using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // duration for car slowing
    public float slowDuration = 5f;

    // trigger enter when touching a power up item
    private void OnTriggerEnter(Collider other)
    {
        // if player comes close to powerup, it destroys all cars in play and slows traffic temporarily
        if (other.CompareTag("Play"))
        {
            // destroys all cars
            foreach (CarMovement car in FindObjectsOfType<CarMovement>()) 
            { 
                Destroy(car.gameObject);
            }
            // sets traffic slow at car spawners for set duration
            CarSpawner spawner = FindObjectOfType<CarSpawner>();
            if (spawner != null)
            {
                spawner.SlowTraffic(slowDuration);
            }
            Destroy(gameObject);
        }
    }
}
