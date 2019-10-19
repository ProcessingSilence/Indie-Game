using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseGame : MonoBehaviour
{
    // Slot for text that tells player that the game is paused.
    public Text pauseText;
    // Dark image slot that fades in when the player dies or wins. It fades out when a scene is loaded.
    public CanvasGroup fadeInImage;
    // Grey image slot that covers the whole screen when paused, this is to prevent the player from exploiting the ability
    // to stop everything by pausing the game.
    public RawImage pauseImage;
    // Boolean that determines if game is paused or not
    private bool isPaused;
    
    // Player transform slot
    private Transform player;
    
    // Integer that determines 
    public int enablePausing = 0;

    // player gameObject slot
    private GameObject playerObject;
    // Start is called before the first frame update

    private bool titleScreen;
    
     
    
    IEnumerator FadeIn()
    {
        // enablePausing has 4 different uses depending on which number it is given
        
        // 0: Fade out after scene starts by 0.1 second increments.
        if (enablePausing == 0)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 11; i++)
            {
                yield return new WaitForSeconds(0.01f);
                fadeInImage.alpha -= .1f;
            }
        }
        
        // 1: Only used to stop 0's if statement from repeating.

        // 2: Fade in after player dies by 0.1 second increments. Once faded in all the way, restart the current scene.
        if (enablePausing == 2)
        {
            for (int i = 0; i < 11; i++)
            {
                yield return new WaitForSeconds(0.01f);
                fadeInImage.alpha += .1f;
            }
            // Create variable currentScene to get the active scene
            var currentScene = SceneManager.GetActiveScene();
            // Create variable currentSceneName to get the scene name from active scene variable
            var currentSceneName = currentScene.name;
            // Load the next scene based on the currentSceneName string
            SceneManager.LoadScene(currentSceneName);
        }

        // 3: Fade in all the way after the player wins by 0.1 second increments. Once fades all the way, load the next scene
        if (enablePausing == 3)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 11; i++)
            {
                yield return new WaitForSeconds(0.01f);
                fadeInImage.alpha += .1f;
            }
            // Create variable currentScene to get the active scene
            var currentScene = SceneManager.GetActiveScene();
            // Create variable currentSceneName to get the scene name from active scene variable
            var currentSceneName = currentScene.name;
            
            // Load the next scene based on the currentSceneName string + "I"
            // Scenes are labeled with Is, to get the next scene name, the letter I is added to the current scene name.
            if (currentSceneName != "title")
            {
                SceneManager.LoadScene(currentSceneName + "I");
            }
            else
            {
                SceneManager.LoadScene("Level I");
            }
            
        }
        // Return null if none of the if statements activate.
        yield return null;
    }

    void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        // Create variable currentSceneName to get the scene name from active scene variable
        var currentSceneName = currentScene.name;

        if (currentSceneName == "title")
        {
            titleScreen = true;
        }

        // Disable pauseText text
        pauseText.GetComponent<Text>().enabled = false;
        // Disable pauseImage image
        pauseImage.GetComponent<RawImage>().enabled = false;
        // Set the alpha of the fadeInImage to 1 to make it opaque.
        fadeInImage.alpha = 1;
    }

    void Update()
    {
        if (titleScreen == true)
        {
            if (Input.anyKey && !Input.GetKeyDown(KeyCode.Escape))
            {
                enablePausing = 3;
            }
        }

        // Start finding the player as the game begins
        // Only fade out if the playerObject slot is null.
        if (playerObject == null && enablePausing == 0)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FadeIn());
            // Set enablePausing to 1 so that this if statement does not repeat.
            enablePausing = 1;
        }

        // If the game has already started and the playerObject is null, use a coroutine to search for the player before
        // determining that the player is dead.
        if (playerObject == null && enablePausing == 1)
        {
            StartCoroutine(waitBeforeDeterminingDead());           
        }

        // Player is confirmed to be dead; fade in and restart the scene.
        if (enablePausing == 2)
        {
            StartCoroutine(FadeIn());
        }

        // Player has won the level; fade in and load next scene.
        if (enablePausing == 3)
        {
            StartCoroutine(FadeIn());
        }
        
        // If ESC is pressed, isPaused is equal to its opposite.
        if (Input.GetKeyDown(KeyCode.Escape) && enablePausing != 2)
            isPaused = !isPaused;

        // If the boolean is false, and the player isn't dead or has won, time moves as normal.
        if (isPaused == false && (enablePausing != 2 || enablePausing != 3))
        {
            Time.timeScale = 1;
            pauseText.GetComponent<Text>().enabled = false;
            pauseImage.GetComponent<RawImage>().enabled = false;
        }
        
        //  ^
        //  |  |    <<< These two conditions switch each time ESC is pressed.
        //     v
        
        // If the boolean is true, and the player isn't dead or has won, time stops.
        else if (isPaused && (enablePausing != 2 || enablePausing != 3))
        {
            pauseText.GetComponent<Text>().enabled = true;
            pauseImage.GetComponent<RawImage>().enabled = true;
            Time.timeScale = 0;
        }
    }

    // Wait 0.1 seconds to see if checkpoint will spawn player before confirming that the player is dead.
    IEnumerator waitBeforeDeterminingDead()
    {
        // After waiting 0.1 seconds, if the player isn't found, enablePausing = 2 to confirm player death.
        yield return new WaitForSeconds(0.1f);
        playerObject = GameObject.FindGameObjectWithTag("Player");
        
        if (playerObject == null)
            enablePausing = 2;
    }


}
