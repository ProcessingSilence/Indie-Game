using UnityEngine;

public class Parenter : MonoBehaviour
{
    // Parent object
    public GameObject myParent;
    // Child object
    public GameObject myChild;

    public GameObject playerHolder;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If 2D collided object tag == "Platform", become a child to that object.
        // This is to prevent the player from sliding off of the platform object, because when it becomes parented to the 
        // platform object, it move around with the platform.
        if (collision.gameObject.tag == "Platform")
        {
            myParent = collision.gameObject;
            playerHolder.transform.parent = myParent.transform;
        }
    }
    
    // When the child object is no longer touching the parent's 2D collider, make the object not a child to that parent.
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            playerHolder.transform.parent = null;
        }
    }
}
