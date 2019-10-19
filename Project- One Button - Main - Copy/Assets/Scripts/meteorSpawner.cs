using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteorSpawner : MonoBehaviour
{
    public GameObject meteor;
    // x location to spawn meteor
    public float xLocation;
    // y location to spawn meteor
    public float yLocation;
    // amount to add to x location
    public float xAdd;
    // amount to add to y location
    public float yAdd;
    
    // x, y, and z rotation values of meteors when spawning
    
    public float xRotation;
    public float yRotation;
    public float zRotation;
    
    // integer that determines how many times the for loop should iterate
    public int meteorAmount;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // for loop that repeats based on meteorAmount integer
            for (var i = 0; i < meteorAmount; i++)
            {
                Instantiate(meteor, new Vector2(xLocation, yLocation), Quaternion.Euler(new Vector3(xRotation, yRotation, zRotation)));

                xLocation += xAdd;
                yLocation += yAdd;
            }
            // Once for loop is done, destroy gameObject. It isn't needed anymore.
            Destroy(gameObject);
        }
    }
}
