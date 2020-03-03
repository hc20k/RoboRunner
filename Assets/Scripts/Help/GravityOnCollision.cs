using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOnCollision : MonoBehaviour
{
    void Start()
    {
        
    }


    // This function is automatically called by Unity when the attached object's
    // collider collides with another object. "Other" is the collider for the object that it collided
    // with.
    private void OnTriggerEnter(Collider other)
    {
        // You should make the colliding object have a tag, so Unity can differentiate between it and something like the floor.
        // How to use tags: https://docs.unity3d.com/Manual/Tags.html
        // When the other object has a tag, you can replace TAG with the tag you made for the object.
        if (other.gameObject.tag == "TAG")
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false; // Accesses the object's rigidbody component and sets Kinematic to false.
            other.gameObject.GetComponent<Rigidbody>().useGravity = true; // Accesses the object's rigidbody component and sets "Use gravity" to true.
        }
    }
}
