using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOAL : MonoBehaviour
{
    // boolean that tells spinner to spin faster
    private bool haveIPlayed;
    // playerHolder slot for gameObject that is parent to player
    public GameObject playerHolder;
    // playerObject slot for the player gameObject itself
    public GameObject playerObject;
    // float variable that determines how fast the gameObject should spin. Positive value spins to left, negative to right.
    private float spinSpeed = -100f;
    // Access gameObject that pauses the game and determines if game should be restarted based on death or win.
    public GameObject pauseObject;
    // Access the pauseGame script from the gameObject that pauses the game to tell it that player has won the level.
    private pauseGame my_pauseGame_script;


    // Start is called before the first frame update
    void Start()
    {
        // stop the particle system at the start, it only needs to run when player touches gameObject
        gameObject.GetComponent<ParticleSystem>().Stop();
        // get the pauseGame script from the pause gameObject.
        my_pauseGame_script = pauseObject.GetComponent<pauseGame>();
    }


    // Update is called once per frame
    void Update()
    {
        // Rotate endlessly, multiply by spinSpeed to control the rotation speed.
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
        
        // spin faster if haveIPlayed boolean is set to true
        if (haveIPlayed)
        {
            // set spin speed to -800
            spinSpeed = -800f;
        }
    }

    // when gameObject enters 3d trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player is touching gameObject and the boolean is set to false (meaning it has not been touched by player
        // before).
        if (other.gameObject.CompareTag("Player") && haveIPlayed == false)
        {
            // Set the haveIPlayed boolean to true first so that the if statement does not repeat
            haveIPlayed = true;
            // play win particle system
            gameObject.GetComponent<ParticleSystem>().Play();
            // play win sound
            gameObject.GetComponent<AudioSource>().Play();            
            // Destroy the PlayerHolder script in playerHolder object to stop it from moving.
            Destroy(playerHolder.GetComponent<PlayerHolder>());
            // Destroy the mesh renderer in the playerObject gameObject to make it look like it dissapeared.
            Destroy(playerObject.GetComponent<MeshRenderer>());
            // set the enablePausing int in the pauseGame script to 3 so that the next stage can be accessed.
            my_pauseGame_script.enablePausing = 3;
        }
    }
}
