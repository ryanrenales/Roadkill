using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float base_speed = 5f; // base speed
    private Rigidbody player; // player rigidbody object
    private Vector3 movement; // movement input variable

    // start
    void Start()
    {
        
        player = GetComponent<Rigidbody>(); // player rigidbody component
    }

    // update
    void Update()
    {
        float x_movement = Input.GetAxisRaw("Horizontal"); // horizontal movement input
        float z_movement = Input.GetAxisRaw("Vertical"); // vertical movement input
        movement = new Vector3(x_movement, 0f, z_movement); // movement input

        
    }

    // fixed update
    private void FixedUpdate()
    {
        player.MovePosition(player.position // player position movement calculation per frame
            + movement  
            * base_speed 
            * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            //later add audio
            //AudioSource.PlayClipAtPoint(deathSound, transform.position);

            ScoreScript.Instance.GameOver();
        }
    }
}
