using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontRotate : MonoBehaviour
{
    Quaternion rotation;
    void Awake()
    {
        // Remember current rotation.
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        // Transform the object's rotation to remembered rotation.
        transform.rotation = rotation;
    }
}
