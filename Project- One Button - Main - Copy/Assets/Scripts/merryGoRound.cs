using UnityEngine;

public class merryGoRound : MonoBehaviour
{
    // CREDIT: https://www.youtube.com/watch?v=mDOU7MWSmoA
    [SerializeField] 
    // Rotation center transform slot
    private Transform rotationCenter;

    
    // Rotation radius and speed values
    
    public float rotationRadius = 2f, angularSpeed = 2f;

    // Pos x and y will be calculated to get desirable position of the moving platform.
    // Angle variable will help find positions x and y.
    private float posX, posY = 0f;
    public float angle = 0f;

    void Update()
    {
        // pos x can be found by using cosine of particular angle.
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        // pos y can be found by using sine of that angle
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        
        //to make the platform move, angle is increased by desired value
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;

        // To keep the angle clear, the value is reset when it becomes greater than 360 degrees.
        if (angle >= 360f)
            angle = 0f;
    }
}
