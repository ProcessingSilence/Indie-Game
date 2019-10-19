using UnityEngine;

public class TestExplosion : MonoBehaviour
{
    // Script to test if mesh will explode.  The triangleExplosion script will not work with meshes with quads or polygons,
    // it only works with meshes consisting of triangular faces.
    void Start()
    {
        gameObject.AddComponent<TriangleExplosion>();
        // Start the coroutine in the triangleExplosion script, which gets the gameObject to shatter. Set destroy to true
        // so that the gameObject gets destroyed as the shattering occurs.
        StartCoroutine(gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
    }
}
