using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemInteraction : MonoBehaviour
{

    public float pickupRange = 2f; // range to be allowed to pick up
    public Transform itemCarryPoint; // transform for the carry point
    public LayerMask itemLayer; // layer for items
    private GameObject heldItem; // variable for the held item
    private ItemProperties heldItemProperties; // variable for the item properties being held
    private float originalSpeed; // original speed before carrying weight
    private PlayerControl movement; // player movement from playercontrol class

    // Start
    void Start()
    {
        // initialize movement as player control component and set original speed
        movement = GetComponent<PlayerControl>();
        if (movement != null)
        {
            originalSpeed = movement.base_speed;
        }
    }

    // Update
    void Update()
    {
        // detects 'E' key to pick up item
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (heldItem == null)
            {
                PickupItem();
            }
            else
            {
                DropItem();
            }

        }
    }

    // function to pick up an item into the transform carry point
    void PickupItem()
    {
        // list of hits, or items detected
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange, itemLayer);

        if (hits.Length > 0)
        {
            // sets item as held item
            GameObject item = hits[0].gameObject;
            heldItem = item;
            heldItemProperties = item.GetComponent<ItemProperties>();

            // sets as rigidbody and kinematic
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            // puts in carry point position
            heldItem.transform.position = itemCarryPoint.position;
            heldItem.transform.SetParent(itemCarryPoint);

            // calculates speed after carrying weight
            if (movement != null && heldItemProperties != null)
            {
                movement.base_speed = originalSpeed * heldItemProperties.speedMultiplier;
            }

        }
    }

    // drops item
    void DropItem()
    {
        if (heldItem != null)
        {
            // sets held item as null and reverts settings to null
            heldItem.transform.SetParent(null);

            Rigidbody rb = heldItem.GetComponent <Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            heldItem = null;
            heldItemProperties = null;

            // sets back to original speed after weight is dropped
            if (movement != null)
            {
                movement.base_speed = originalSpeed;
            }
        }
    }
    
    // triggers the item to drop once entered in a zone
    private void OnTriggerEnter(Collider other)
    {
        // if nearby house, item is dropped
        if (other.CompareTag("House") && heldItem != null) 
        {
            ScoreScript.Instance.AddScore(heldItemProperties.score); // addds score
            Destroy(heldItem); // destroys item
            DropItem(); // drops it

            ItemSpawner.Instance.SpawnItem(); // spawns new item across the street
        }
    }


}
