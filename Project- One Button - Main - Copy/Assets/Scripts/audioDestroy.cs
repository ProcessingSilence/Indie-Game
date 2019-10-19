using System.Collections;
using UnityEngine;

public class audioDestroy : MonoBehaviour
{
    // audioSource slot
    private AudioSource audioPlayer; 

    // Start is called before the first frame update
    void Start()
    {
        // Get AudioSource component from gameObject
        audioPlayer = GetComponent<AudioSource>();       
        audioPlayer.Play();
        //starts IEnumerator DestroyMyself() to destroy the sound object after 2 seconds.
        StartCoroutine(DestroyMyself());
    }

    //IEnumerator that destroys the gameObject after 1 second
    private IEnumerator DestroyMyself()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}