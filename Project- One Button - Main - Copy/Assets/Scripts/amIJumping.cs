using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amIJumping : MonoBehaviour
{
    // Determines jump height of player
    public float jumpHeight = 5f;

    private PlayerHolder my_playerHolder_script;

    public GameObject PlayerHolderObject;
    // Rigidbody2D slot
    private Rigidbody2D rb2D;

    private AudioSource boingSound;

    // Boolean that prevents player from pressing button multiple times while on jump pad to gain infinite height;
    private bool stopJump;
    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody2D of gameObject
        rb2D = GetComponent<Rigidbody2D>();
        my_playerHolder_script = PlayerHolderObject.GetComponent<PlayerHolder>();
        boingSound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        // If the boolean isJumping in the playerHolder script is true, check to see if stopJump is not true. If both apply,
        // add force in the y direction of the player's Rigidbody2D.
        if (my_playerHolder_script.isJumping)
        {
            if (Input.anyKey && !Input.GetKeyDown(KeyCode.Escape) && stopJump == false)
            {
                StartCoroutine(noJumpExploit());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // if touching a gameObject with tag "Jump", a force to the y of the Rigidbody2D will be added based on jumpHeight value.
        if (other.gameObject.CompareTag("Jump"))
        {
            my_playerHolder_script.isJumping = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        // if touching a gameObject with tag "Jump", a force to the y of the Rigidbody2D will be added based on jumpHeight value.
        if (other.gameObject.CompareTag("Jump"))
        {
            my_playerHolder_script.isJumping = false; 
        }
    }

    // stopJump becomes true for 0.1 seconds to prevent player from hitting jump button multiple times.
    IEnumerator noJumpExploit()
    {
        stopJump = true;
        boingSound.Play();
        Debug.Log("Input");
        rb2D.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);   
        yield return new WaitForSeconds(0.1f);
        stopJump = false;
    }
}
