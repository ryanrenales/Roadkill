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

    Animator anim;

    // start
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

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

        if (velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        anim.SetFloat("Vert", velocity.magnitude);
    }

    // fixed update
    private void FixedUpdate()
    {
        player.MovePosition(player.position // player position movement calculation per frame  
            + velocity 
            * Time.fixedDeltaTime);

        Vector3 pos = player.position;
        pos.x = Mathf.Clamp(pos.x, -12f, 12f);
        pos.z = Mathf.Clamp(pos.z, -10f, 5.8f);
        player.position = pos;
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
