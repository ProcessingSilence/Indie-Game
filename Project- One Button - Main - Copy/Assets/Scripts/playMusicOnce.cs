using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playMusicOnce : MonoBehaviour
{
    private bool hasMusicPlayed;
    private AudioSource musicPlayer;
    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            if (hasMusicPlayed == false)
            {
                hasMusicPlayed = true;
                musicPlayer.Play();
            }
        }
    }
}
