using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{    
    
    // Number to remember moveSpeed's value
    public float tempNumber;
    // Movement speed of player
    public float moveSpeed = 0f;
    // Boolean to detect if button is held down
    public bool isHeldDown;
    // Player transform slot
    public Transform player;
    // Player gameObject slot
    public GameObject playerObject;
    // Deathsound gameObject slot
    public GameObject deathSound;

    public GameObject moveSound;

    public GameObject stopSound;
    // Float variable that determines what y value considers that the player is falling off of the stage, and should by dying.
    public float fallKill;
    // Boolean that determines if the player has died already or not so that if statements do not repeat or are not accessed.
    private bool hasPlayerDied;
    // Player emits sparkles when moving.
    public ParticleSystem playerParticles;
    
    // Bool that determines if player is jumping or not.
    public bool isJumping;
    
    // Aesthetic lights gameObjects
    public GameObject lightObject1;
    public GameObject lightObject2;
    public GameObject lightObject3;
    public GameObject lightObject4;
    public GameObject lightObject5;
    
    // Aesthetic lights Lights
    private Light playerLight1;
    private Light playerLight2;
    private Light playerLight3;
    private Light playerLight4;
    private Light playerLight5;
    
    private bool audioIsPlaying;

    void Start()
    {
        // Get light components in the aesthetic light objects
        playerLight1 = lightObject1.GetComponent<Light>();
        playerLight2 = lightObject2.GetComponent<Light>();
        playerLight3 = lightObject3.GetComponent<Light>();
        playerLight4 = lightObject4.GetComponent<Light>();
        playerLight5 = lightObject5.GetComponent<Light>();

        // Play particle system at start
        playerParticles.Play();


    }
    
    // Update is called once per frame
    void Update()
    {
        // if the player is non-existant, stop moving.
        if (player == null)
        {
            // set tempNumber and moveSpeed to 0 so that gameObject does not move after the player dies
            tempNumber = 0;
            moveSpeed = 0;
            // if the player is dead, destroy the Rigidbody2D so physical movement cannot be made.
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            var emission = playerParticles.emission;
            emission.rateOverTime = 0;
        }
        
        // Move to the right forever at a rate of the moveSpeed variable in the Vector2 area.
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
        
        // If any key is held down (besides Esc because that pauses the game), set the isHeldDown bool to true
        if (Input.anyKey && !Input.GetKeyDown(KeyCode.Escape))
            isHeldDown = true;
        // If any key is not held down, set isHeldDown bool to false 
        else
            isHeldDown = false;

        // When moveSpeed is considered "off", its moveSpeed is set to 0. 
        // Also turns the light on or off by -0.1 or +0.1 increments to have it turn off or on smoothly (and to prevent
        // epilepsy with flashing lights).
        if (isHeldDown && isJumping != true)
        {
            moveSpeed = 0;  
            if (audioIsPlaying == false && isJumping != true)
            {
                audioIsPlaying = true;
                Instantiate(stopSound);
            }
            if (Math.Abs(playerLight1.intensity) > 0)
            {
                playerLight1.intensity -= 0.5f;
                playerLight2.intensity -= 0.5f;
                playerLight3.intensity -= 0.5f;
                playerLight4.intensity -= 0.5f;
                playerLight5.intensity -= 0.5f;
                var emission = playerParticles.emission;
                emission.rateOverTime = 0;
            }          
        }
        else
        {            
            moveSpeed = tempNumber;
            if (audioIsPlaying == true && isJumping != true)
            {
                audioIsPlaying = false;
                Instantiate(moveSound);
            }
            if (Math.Abs(playerLight1.intensity) < 2)
            {
                playerLight1.intensity += 0.5f;
                playerLight2.intensity += 0.5f;
                playerLight3.intensity += 0.5f;
                playerLight4.intensity += 0.5f;
                playerLight5.intensity += 0.5f;
                var emission = playerParticles.emission;
                emission.rateOverTime = 10;
            }
        }

        // If the player's y transform position is less than the fallKill variable, kill the player.
        if (player.position.y < fallKill)
        {           
            if (hasPlayerDied == false)
            {
                // Boolean varible that determines if player died set to true so if statement doesn't repeat.
                hasPlayerDied = true;
                // set tempNumber and moveSpeed to 0 so that gameObject does not move as it dies
                tempNumber = 0;
                moveSpeed = 0;
                // Add the script to the player gameObject that makes it shatter.
                playerObject.gameObject.AddComponent<TestExplosion>();       
                // instantiate the gameObject that plays the death sound
                Instantiate(deathSound);
            }           
        }
    }
}
