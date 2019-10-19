using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPlayerOnAwake : MonoBehaviour
{
    private GameObject playerObject;
    // Start is called before the first frame update
    void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        Destroy(playerObject);
        Destroy(gameObject);
    }
}