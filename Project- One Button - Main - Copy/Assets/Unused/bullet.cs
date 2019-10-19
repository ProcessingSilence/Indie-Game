using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    public float speed = 10f;
    public GameObject PointObject;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        transform.Translate(speed, 0, 0);
    }
    void OnBecameInvisible()
    {
        StartCoroutine(destroyMe());
    }

    IEnumerator destroyMe()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
