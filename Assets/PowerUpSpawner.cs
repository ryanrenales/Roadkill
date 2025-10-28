using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    public GameObject powerupPrefab;
    public float minX = -8f;
    public float maxX = 8f;
    public float zStart = -20f;
    public float zEnd = 20f;
    public float spawnInterval = 10f;

    private GameObject currentPowerup;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnPowerup();
        InvokeRepeating(nameof(CheckAndRespawn), spawnInterval, spawnInterval);
    }

    void SpawnPowerup()
    { 
        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(zStart, zEnd));
        currentPowerup = Instantiate(powerupPrefab, spawnPos, Quaternion.identity);
    }

    void CheckAndRespawn()
    {
        if (currentPowerup == null) 
        {
            SpawnPowerup();
        }
    }
}
