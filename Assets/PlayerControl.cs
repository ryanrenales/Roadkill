using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    public float base_speed = 5f; // base speed
    private Rigidbody player; // player rigidbody object
    private Vector3 movement; // movement input variable

    // variables for the stamina mechanic
    public float maxStamina = 100f;
    public float currentStamina;
    public float sprintSpeed = 8f;
    public float staminaDrain = 20f;
    public float staminaRegen = 15f;
    public Slider staminaBar;

    private Vector3 velocity; // current velocity

    Animator anim; // animation for player

    // audio variables for walking
    private AudioSource audioSource;
    public AudioClip footstep;
    public float stepInterval = 0.4f;
    private float stepTimer = 0f;

    // start
    void Start()
    {
        anim = GetComponentInChildren<Animator>(); // animation component for the chicken

        player = GetComponent<Rigidbody>(); // player rigidbody component
        currentStamina = maxStamina; // sets max stamina
        if (staminaBar != null)
        {
            staminaBar.value = currentStamina;
        }

        audioSource = GetComponent<AudioSource>(); // audio source component
    }

    // update
    void Update()
    {
        // raw axis direction input for direction
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        // if left shift is down, sprinting would be true
        bool sprinting = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f && direction.magnitude > 0f;
        float speed = sprinting ? sprintSpeed : base_speed; // if sprinting, sets speed to sprint speed

        velocity = direction * speed; // velocity calculation

        // if sprinting, drain stamina overtime, else regen stamina if not sprinting and standing still
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

        // if moving, player rotates in direction, and walking audio is played while moving to each footstep
        if (velocity.magnitude > 0.1f)
        {
            // player rotation
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

            // audio for walking timed and calculated
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f) 
            {
                audioSource.PlayOneShot(footstep);
                stepTimer = stepInterval / (velocity.magnitude / base_speed);
            }
        }
        else
        {
            stepTimer = 0f;
        }

            anim.SetFloat("Vert", velocity.magnitude); // sets animation

    }

    // fixed update
    private void FixedUpdate()
    {
        player.MovePosition(player.position // player position movement calculation per frame  
            + velocity 
            * Time.fixedDeltaTime);

        // clamps player position to stay in screen
        Vector3 pos = player.position;
        pos.x = Mathf.Clamp(pos.x, -12f, 12f);
        pos.z = Mathf.Clamp(pos.z, -10f, 5.8f);
        player.position = pos;
    }

    // game over when player collides with any cars
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            ScoreScript.Instance.GameOver();
        }
    }
}
