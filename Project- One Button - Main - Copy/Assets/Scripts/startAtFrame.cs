using UnityEngine;
     
public class startAtFrame : MonoBehaviour {
    // animator slot
    Animator anim;
    // Starting time of animation from 0 to 1.     Ex. 0.25 is 1/4 of the animation, 0 beginning, 1 very end, 0.5 half, etc.
    public float animationTime = 0;
    // Speed of animation.     Ex. 0.5 = 0.5 times fast, 2 = 2 times fast, 1 = 1 times fast, etc.
    public float animationSpeed = 1f;

    // Public name of animation to play; set to crusher animation by default, can be changed in editor
    public string animationName = "crusher";
    
    void Start () {
        // Get animator from gameObject
        anim = GetComponent<Animator>();
        // Set the animation speed to match animation speed variable.
        anim.speed = animationSpeed;
        // Play the animation under the string variable, with the time to start at under animationTime variable.
        anim.Play(animationName, 0, animationTime);
    }
}