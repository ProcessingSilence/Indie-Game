using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    
    // Number to remember moveSpeed's value
    public float tempNumber;
    // Movement speed of player
    public float moveSpeed = 0f;
    // Boolean to detect if button is held down
    public bool isHeldDown;

    // Aesthetic lights gameObjects
    public GameObject lightObject1;
    public GameObject lightObject2;
    public GameObject lightObject3;
    public GameObject lightObject4;
    public GameObject lightObject5;

    // GameObject that plays death sound
    public GameObject deathSound;
    
    // Aesthetic lights Lights
    private Light playerLight1;
    private Light playerLight2;
    private Light playerLight3;
    private Light playerLight4;
    private Light playerLight5;
    
    // Animator slot (unused)
    //private Animator anim;
    
    // Boolean that makes sure animation if statement only plays once and does not repeat (unused)
    //private bool playOnce;
    void Start()
    {
        // tempNumber is equal to the moveSpeed, so that when the moveSpeed variable becomes 0 and needs to go back to normal,
        // it can just access the tempNumber's value.
        tempNumber = moveSpeed;
        // Get light components in the aesthetic light objects
        playerLight1 = lightObject1.GetComponent<Light>();
        playerLight2 = lightObject2.GetComponent<Light>();
        playerLight3 = lightObject3.GetComponent<Light>();
        playerLight4 = lightObject4.GetComponent<Light>();
        playerLight5 = lightObject5.GetComponent<Light>();
        // Get animator component in gameObject (unused)
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move to the right forever at a rate of the moveSpeed variable in the Vector2 area.
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.right);
        
        // If any key is held down (besides Esc because that pauses the game), set the isHeldDown bool to true
        if (Input.anyKey && !Input.GetKeyDown(KeyCode.Escape))
            isHeldDown = true;
        // If any key is not held down, set isHeldDown bool to false 
        else
            isHeldDown = false;

        // When moveSpeed is considered "off", its moveSpeed is set to 0. 
        if (isHeldDown)
            moveSpeed = 0;
            // Unused animation, this might get reused in the future.
            /*
            if (playOnce == false)
            {
               
                //anim.SetBool("isDucking", true);
                //anim.SetBool("isStanding", false);
                playOnce = true;
            }
            */

        // Turn moveSpeed off or on based on isHeldDown boolean, also turns the light on or off by -0.1 or +0.1 increments
        // to have it turn off or on smoothly (and to prevent epilepsy with flashing lights).
        if (Math.Abs(playerLight1.intensity) > 0)
        {
            playerLight1.intensity -= 0.5f;
            playerLight2.intensity -= 0.5f;
            playerLight3.intensity -= 0.5f;
            playerLight4.intensity -= 0.5f;
            playerLight5.intensity -= 0.5f;
        }
        // When moveSpeed is "on", it's set to it's remembered speed value in tempNumber.
        else
        {
            // Unused animation, this might get reused in the future.
            /*
            if (playOnce)
            {
                //anim.SetBool("isDucking", false);
                //anim.SetBool("isStanding", true);
                playOnce = false;
            }
            */
            moveSpeed = tempNumber;
            if (Math.Abs(playerLight1.intensity) < 2)
            {
                playerLight1.intensity += 0.5f;
                playerLight2.intensity += 0.5f;
                playerLight3.intensity += 0.5f;
                playerLight4.intensity += 0.5f;
                playerLight5.intensity += 0.5f;
            }
        }
        
        // Detect if player touches "enemy", if so, destroy the player and start triangle explosion
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Detect if touching gameObject is labled with tag "Enemy"
        if (other.gameObject.CompareTag("Enemy"))
        {
            // set tempNumber and moveSpeed to 0 so that gameObject does not move as it dies
            tempNumber = 0;
            moveSpeed = 0;
            // debug to log to show that the collision is detected
            // Debug.Log("HIT!");
            
            // Destroy all children in gameObject so that the lights do not show as the player dies
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }
            // Instantiate the death sound
            Instantiate(deathSound);
            // Destroy gameObject's box collider so collision does not get made after the player dies
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            // Destroy the Rigidbody2D so that the gameObject does not make physical motion after it dies
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            // Add the script that makes the gameObject shatter.
            gameObject.AddComponent<TestExplosion>();
        }
    }
}
