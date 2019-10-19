using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textChange : MonoBehaviour
{
    // CREDIT: https://unitycoder.com/blog/2015/12/03/ui-text-typewriter-effect-script/
    private Text instructions;
    public string story;
    private int storyPage = 0 ;

    void Awake () 
    {
        instructions = GetComponent<Text> ();        
    }

    void Update()
    {
        if (storyPage == 1)
        {
            story = "\r\n In this game, there is only one input option\r\navailable to control the player...\r\n Any button on the keyboard";
            instructions.text = "";
            StartCoroutine ("PlayText");
        }

        if (storyPage == 2)
        {
            story = "\r\n In this game, there is only one input option\r\navailable to control the player...\r\n Any button on the keyboard";
            instructions.text = "";
            StartCoroutine ("PlayText");
        }
    }

    IEnumerator PlayText()
    {
        foreach (char c in story) 
        { 
            instructions.text += c;
            if (c.Equals(','))
                yield return new WaitForSeconds (0.125f);
            if (c.Equals('.'))
                yield return new WaitForSeconds (0.25f);
            yield return new WaitForSeconds (0.075f);
        }
    }
}
