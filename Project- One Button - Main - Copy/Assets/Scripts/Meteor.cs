using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Meteor speed
    public float meteorSpeed = 10f;
    // Bool determining if meteor moves left or right. True = Left. False = Right.
    public bool leftOrRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftOrRight = true)
            transform.Translate(meteorSpeed * Time.deltaTime * Vector2.left);
        
        if (leftOrRight = false)
            transform.Translate(meteorSpeed * Time.deltaTime * Vector2.right);
    }
}
