using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rotateForever : MonoBehaviour
{
    // float variable that determines how fast the gameObject should spin. Positive value spins to left, negative to right.
    public float spinSpeed = -800f;

    // Update is called once per frame
    void Update()
    {
        // Rotate endlessly, multiply by spinSpeed to control the rotation speed.
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
