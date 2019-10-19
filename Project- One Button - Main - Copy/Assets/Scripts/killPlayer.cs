using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    public GameObject killSound;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D other)
    {
        // If the colliding object is tagged "Player"...
        if (other.gameObject.tag == "Player")
        {
            // Debug to log that the hit with player is detected
            Debug.Log("HIT!");
            // Destroy all the children within the player so that the lights attached will not glow while triangle
            // explosion occurs.
            foreach (Transform child in other.transform)
            {
                Destroy(child.gameObject);
            }
            // Destroy these components so movement cannot be made while player is dead:
            // Destroy BoxCollider2D so collisions are not made.
            Destroy(other.gameObject.GetComponent<BoxCollider2D>());
            // Destroy Rigidbody2D so that no physical movement occurs.
            Destroy(other.gameObject.GetComponent<Rigidbody2D>());
            // Destroy PlayerMovement script so the automatic movement stops.
            Destroy(other.gameObject.GetComponent<PlayerMovement>());
            
            // Add the explosion script to the playerObject.
            other.gameObject.AddComponent<TestExplosion>();

            Instantiate(killSound);
        }
    }

}
