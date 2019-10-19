using UnityEngine;

public class audioPlay : MonoBehaviour
{
    // audioSource slot
    private AudioSource audioPlayer; 

    // Start is called before the first frame update
    void Awake()
    {
        // get audioSource from gameObject
        audioPlayer = GetComponent<AudioSource>();        
    }

    // function to play audio sound, gets called by animators
    void playAudio()
    {
        audioPlayer.Play();
    }
}
