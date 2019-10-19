using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isTouchingWall : MonoBehaviour
{
    public GameObject playerHolderObject;
    private PlayerHolder my_PlayerHolder_script;

    private float getTempNumber;
    // Start is called before the first frame update
    void Start()
    {
        my_PlayerHolder_script = playerHolderObject.GetComponent<PlayerHolder>();
        getTempNumber = my_PlayerHolder_script.tempNumber;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(("Wall")))
        {
            my_PlayerHolder_script.tempNumber = 0;
        }
    }
    
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(("Wall")))
        {
            my_PlayerHolder_script.tempNumber = getTempNumber;
        }
    }
}
