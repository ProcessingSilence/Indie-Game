using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class checkpoint : MonoBehaviour
{
    private bool haveIPlayed;
    public GameObject playerHolder;
    private float spinSpeed = -100f;

    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);


		if (haveIPlayed)
		{
			spinSpeed = -800f;
		}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && haveIPlayed == false)
        {
            haveIPlayed = true;
            gameObject.tag = "Checked";
            gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<AudioSource>().Play();            
            Destroy(playerHolder.GetComponent<PlayerHolder>());
        }

        if (other.gameObject.CompareTag("Checked"))
        {
            Destroy(gameObject);
        }        
    }
}
