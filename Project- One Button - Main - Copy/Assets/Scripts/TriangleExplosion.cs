using UnityEngine;
using System.Collections;
 
// CREDIT: https://answers.unity.com/questions/1006318/script-to-break-mesh-into-smaller-pieces.html
/*Quotes from author:
 * 
 *"Here is a script that will create seperate triangle rigidbodies from the gameObject's mesh
 * and make them explode, for a nice shattering effect. Works great for low-poly object, but
 * might lead to performancy issues with medium-to high poly count objects. I think I found it
 * on the unity wiki."
 *
 * "Here is a script that makes an object explode into it's triangles.. Add the component to a gameobject and
 * call the method with a bool parameter (controls if the objects should be destroyed)"
 */

// I had to do a bit of research to understand how this code works, this video helped me understand it a bit:
// https://youtu.be/gmuHI_wsOgI
 public class TriangleExplosion : MonoBehaviour
 {
     public IEnumerator SplitMesh (bool destroy)    {
 
         // if there's no mesh filter, then the script can't do anything
         if(GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null) {
             yield return null;
         }
         
         // disable the gameObject's collider
         if(GetComponent<Collider>()) {
             GetComponent<Collider>().enabled = false;
         }
         // create a new mesh
         Mesh M = new Mesh();
         if(GetComponent<MeshFilter>()) {
         // M gets the mesh filter of the gameObject
             M = GetComponent<MeshFilter>().mesh;
         }
         // M gets the skinned mesh filter of the gameObject if it doesn't have a mesh filter
         else if(GetComponent<SkinnedMeshRenderer>()) {
             M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
         }
         // make a new material array, materials
         Material[] materials = new Material[0];
         // materials gets the materials from the MeshRenderer
         if(GetComponent<MeshRenderer>()) {
             materials = GetComponent<MeshRenderer>().materials;
         }
         // materials gets the materials from the SkinnedMeshRenderer if it doesn't have a MeshRenderer
         else if(GetComponent<SkinnedMeshRenderer>()) {
             materials = GetComponent<SkinnedMeshRenderer>().materials;
         }
         // get all verticies of M model within an array
         Vector3[] verts = M.vertices;
         // get all normals of M model within an array
         Vector3[] normals = M.normals;
         // get all uvs of M model within an array
         Vector2[] uvs = M.uv;
         //get submeshes to apply materials to triangle shards within a for loop for the amount of triangle shards
         for (int submesh = 0; submesh < M.subMeshCount; submesh++) {
         // from Unity Wiki: "Fetches the triangle list for the specified sub-mesh on this object."
             int[] indices = M.GetTriangles(submesh);
         // for loop that iterates based on the number length of the indices.
             for (int i = 0; i < indices.Length; i += 3)    {
                 // create a new Vector3 array 
                 Vector3[] newVerts = new Vector3[3];
                 Vector3[] newNormals = new Vector3[3];
                 Vector2[] newUvs = new Vector2[3];
                 // array that iterates 3 times to get the 3 values of the verts, uvs, and normals in the new vector3s
                 for (int n = 0; n < 3; n++)    
                 {                
                     int index = indices[i + n];
                     // get newVerts array value based on for loop
                     newVerts[n] = verts[index];
                     // get newUvs array value based on for loop
                     newUvs[n] = uvs[index];
                     // get newNormals array value based on for loop
                     newNormals[n] = normals[index];
                 }
                 // create new mesh variable to create the triangle shard
                 Mesh mesh = new Mesh();
                 // get the verticies, normals, and uvs of the 'new' values
                 mesh.vertices = newVerts;
                 mesh.normals = newNormals;
                 mesh.uv = newUvs;
                 
                 // set triangle vertices in a way so that there are faces on both the front and back sides of the model(?)
                 mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };
                 // create new gameObject for triangle, with number label 
                 GameObject GO = new GameObject("Triangle " + (i / 3));
                 // Set the layer of the new gameObject to 'Particle' layer
                 GO.layer = LayerMask.NameToLayer("Particle");
                 // set the transform position of new gameObject to current gameObject position
                 GO.transform.position = transform.position;
                 // set the transform rotation of new gameObject to current gameObject rotation
                 GO.transform.rotation = transform.rotation;
                 // Add mesh renderer and add materials from submesh to new gameObject
                 GO.AddComponent<MeshRenderer>().material = materials[submesh];
                 // Add mesh filter and add new mesh to new gameObject
                 GO.AddComponent<MeshFilter>().mesh = mesh;
                 // add box collider to new gameObject
                 GO.AddComponent<BoxCollider>();
                 // shoot out new gameObject in random direction from -0.5 to 0.5 on xyz axis
                 Vector3 explosionPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z + Random.Range(-0.5f, 0.5f));
                 // add rigidbody and an explosion force direction to new gameObject
                 GO.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(1600,2000), explosionPos, 15);
                 // destroy the new gameObject after being delayed by 5 seconds + random seconds from 0 to 5
                 Destroy(GO, 5 + Random.Range(0.0f, 5.0f));
             }
         }
         // disable renderer of gameObject
         GetComponent<Renderer>().enabled = false;
         
         //wait 1 second before going through destroy object if statement
         yield return new WaitForSeconds(1.0f);
         // destroy the gameObject if destroy is true
         if(destroy == true) {
             Destroy(gameObject);
         }
     }
 }