using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float base_speed = 5f; // car base speed
    public Vector3 direction = Vector3.forward; // direction for car to move
    public float RoadLimit = 10f; // end of screen limit to destroy object
    private Rigidbody car; // car rigidbody object

    private AudioSource engine; // engine audio


    // start
    void Start()
    {

        car = GetComponent<Rigidbody>(); // car rigidbody component
        engine = GetComponent<AudioSource>(); // audio source component
        if (engine != null)
        {
            engine.loop = true; // plays engine sound on loop with the car
            engine.Play(); // plays sound
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        car.MovePosition(car.position // car position movement calculation per frame
            + direction 
            * base_speed 
            * Time.fixedDeltaTime);

        // if car's position is past road limit, destroy car
        if (Mathf.Abs(transform.position.x) > RoadLimit)
        {
            Destroy(gameObject);
        }

        // if engine and car exists, engine sound is played with pitch adjustments according to velocity
        if (engine != null && car != null) 
        { 
            float factor = car.velocity.magnitude / 10f;
            engine.pitch = Mathf.Clamp(0.8f + factor, 0.8f, 1.5f);
        }
    }
}
