using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject powerupPrefab; // prefab for powerup
    // spawn borders so it stays in frame
    public float minX = -8f;
    public float maxX = 8f;
    public float zStart = -20f;
    public float zEnd = 20f;
    public float spawnInterval = 10f; // spawn interval

    private GameObject currentPowerup; // current powerup variable

    // Start
    void Start()
    {
        // when it starts, no powerup is spawned until after the spawn interval
        // it will repeat every interval and the check and respawn makes sure there's only one powerup on the floor at a time
        InvokeRepeating(nameof(CheckAndRespawn), spawnInterval, spawnInterval);
    }

    // spawns the powerup
    void SpawnPowerup()
    { 
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(zStart, zEnd)); // range for random spawn position
        currentPowerup = Instantiate(powerupPrefab, spawnPos, Quaternion.identity); // instantiates the current powerup on the screen
    }

    // checks and makes sure there's only one powerup at a time on the screen
    void CheckAndRespawn()
    {
        if (currentPowerup == null) 
        {
            SpawnPowerup();
        }
    }
}
