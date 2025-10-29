using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    // empty list for item prefabs and spawnpoints
    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;

    // instance for the spawner
    public static ItemSpawner Instance;

    // awake
    void Awake()
    {
        Instance = this;

    }
    
    private void Start()
    {
        // spawns 3 items at the start
        for (int i = 0; i < 3; i++) 
        {
            SpawnItem();
        }
       
    }

   
    // spawn item function spawns an item in a space that isn't taken
    public void SpawnItem()
    {
        // check if null
        if(itemPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }

        List<Transform> availablePoints = new List<Transform>(); // list for available points

        // for each point in spawn points, it will check if occupied
        foreach (Transform point in spawnPoints)
        {
            Collider[] hits = Physics.OverlapSphere(point.position, 0.5f);
            bool occupied = false;
            foreach (var hit in hits)
            {
                if (hit.CompareTag("LightItem") || hit.CompareTag("MediumItem") || hit.CompareTag("HeavyItem"))
                {
                    occupied = true;
                    break;
                }
            }
            if (!occupied) // if not occupied then add to available points
            {
                availablePoints.Add(point);
            }
        }
            // instantiates prefab in the spawn points
            if(availablePoints.Count > 0)
            {
                Transform spawnPoint = availablePoints[Random.Range(0, availablePoints.Count)];
                GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            }
    }

}
