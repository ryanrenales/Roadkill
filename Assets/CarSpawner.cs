using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    public GameObject[] carPrefabs; // car prefab list
    public Transform[] spawnPoints; // spawn points list
    public float interval = 2f; // spawn intervals
    public float variance = 0.5f; // variance factor
    public float minSpeed = 5f; // minimum speed
    public float maxSpeed = 10f; // maximum speed
    private float spawnTime; // spawn time variable for the next car spawn

    // Start
    void Start()
    {
        spawnTime = Time.time + interval + Random.Range(-variance, variance); // calculates next spawn time
    }

    // Update
    void Update()
    {
        if (Time.time >= spawnTime) // if it's time to spawn a car
        {
            int randomLane = Random.Range(0, spawnPoints.Length); // picks random lane
            spawnCar(randomLane); // spawns car in random lane

            spawnTime = Time.time + interval + Random.Range(-variance, variance); // calculates next spawn time
        }

    }

    // function to spawn car in the chosen random lane
    void spawnCar(int lane)
    {
        // if there is no prefabs or spawn points, return
        if (carPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }
        // if lane # doesn't exist then return
        if (lane < 0 || lane >= spawnPoints.Length)
        {
            return;
        }

        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)]; // assigns a random car prefab from the list
        Transform spawnPoint = spawnPoints[lane]; // assigns spawnpoint to the given lane

        GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation); // instantiates a car object to spawn with the given parameters
        CarMovement movement = car.GetComponent<CarMovement>(); // gets car movement component for the car to move

        // if movement isn't null, sets the car movement base speed at random, and the direction to move forward from spawn
        if (movement != null)
        {
            movement.base_speed = Random.Range(minSpeed, maxSpeed);
            movement.direction = spawnPoint.forward;
        }
    }

}
