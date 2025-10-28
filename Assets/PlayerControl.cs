using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float base_speed = 5f; // base speed
    private Rigidbody player; // player rigidbody object
    private Vector3 movement; // movement input variable

    public float maxStamina = 100f;
    public float currentStamina;
    public float sprintSpeed = 8f;
    public float staminaDrain = 20f;
    public float staminaRegen = 15f;
    public Slider staminaBar;

    private Vector3 velocity;

    // start
    void Start()
    {
        
        player = GetComponent<Rigidbody>(); // player rigidbody component
        currentStamina = maxStamina;
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }
    }

    // update
    void Update()
    {

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        bool sprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f && direction.magnitude > 0f;
        float speed = sprinting ? sprintSpeed : base_speed;

        velocity = direction * speed;

        if (sprinting)
        {
            currentStamina = Mathf.Max(currentStamina - staminaDrain * Time.deltaTime, 0f);
        }
        else if(direction.magnitude == 0f)
        {
            currentStamina = Mathf.Min(currentStamina + staminaRegen * Time.deltaTime, maxStamina);
        }

        if (staminaBar != null) 
        {
            staminaBar.value = currentStamina;
        }
    }

    // fixed update
    private void FixedUpdate()
    {
        player.MovePosition(player.position // player position movement calculation per frame  
            + velocity 
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
