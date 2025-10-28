using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemInteraction : MonoBehaviour
{

    public float pickupRange = 2f;
    public Transform itemCarryPoint;
    public LayerMask itemLayer;
    private GameObject heldItem;
    private ItemProperties heldItemProperties;
    private float originalSpeed;
    private PlayerControl movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerControl>();
        if (movement != null)
        {
            originalSpeed = movement.base_speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

    void PickupItem()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRange, itemLayer);

        if (hits.Length > 0)
        {
            GameObject item = hits[0].gameObject;
            heldItem = item;
            heldItemProperties = item.GetComponent<ItemProperties>();

            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            heldItem.transform.position = itemCarryPoint.position;
            heldItem.transform.SetParent(itemCarryPoint);

            if (movement != null && heldItemProperties != null)
            {
                movement.base_speed = originalSpeed * heldItemProperties.speedMultiplier;
            }

        }
    }

    void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.transform.SetParent(null);

            Rigidbody rb = heldItem.GetComponent <Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            heldItem = null;
            heldItemProperties = null;

            if (movement != null)
            {
                movement.base_speed = originalSpeed;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("House") && heldItem != null) 
        {
            ScoreScript.Instance.AddScore(heldItemProperties.score);
            Destroy(heldItem);
            DropItem();

            ItemSpawner.Instance.SpawnItem();
        }
    }


}
