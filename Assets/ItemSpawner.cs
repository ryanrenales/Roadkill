using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject[] itemPrefabs;
    public Transform[] spawnPoints;

    public static ItemSpawner Instance;

    void Awake()
    {
        Instance = this;

    }
    
    private void Start()
    {
        for (int i = 0; i < 3; i++) 
        {
            SpawnItem();
        }
       
    }

   

    public void SpawnItem()
    {
        if(itemPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }

        List<Transform> availablePoints = new List<Transform>();

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
            if (!occupied)
            {
                availablePoints.Add(point);
            }
        }

            if(availablePoints.Count > 0)
            {
                Transform spawnPoint = availablePoints[Random.Range(0, availablePoints.Count)];
                GameObject prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            }
    }

}
