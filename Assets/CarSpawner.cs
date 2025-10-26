using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public GameObject[] carPrefabs;
    public Transform[] spawnPoints;
    public float interval = 2f;
    public float variance = 0.5f;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;
    private float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time + interval + Random.Range(-variance, variance);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= spawnTime)
        {
            int randomLane = Random.Range(0, spawnPoints.Length);
            spawnCar(randomLane);

            spawnTime = Time.time + interval + Random.Range(-variance, variance);
        }

    }

    void spawnCar(int lane)
    {
        if (carPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }
        if (lane < 0 || lane >= spawnPoints.Length)
        {
            return;
        }

        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];
        Transform spawnPoint = spawnPoints[lane];

        GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);
        CarMovement movement = car.GetComponent<CarMovement>();

        if (movement != null)
        {
            movement.base_speed = Random.Range(minSpeed, maxSpeed);
            movement.direction = spawnPoint.forward;
        }
    }

}
