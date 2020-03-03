using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    // Make sure this is attached on the object you want to dissapear

    // This function is automatically called by Unity when the attached object's
    // collider collides with another object. "other" is the collider for the object that it collided
    // with.
    private void OnTriggerEnter(Collider other)
    {
        // You should make the colliding object have a tag, so Unity can differentiate between it and something like the floor.
        // How to use tags: https://docs.unity3d.com/Manual/Tags.html
        // When the other object has a tag, you can replace Player with the tag you made for the object.
        if (other.gameObject.tag == "Player")
        {
           GetComponent<MeshRenderer>().enabled = false; // Disables this object's mesh renderer, making it invisible.
        }
    }

    // This function is automatically called by Unity when the attached object's
    // collider STOPS colliding with another object. "other" is the collider for the object that it stopped colliding
    // with.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = true; // Enables this object's mesh renderer, making it visible again.
        }
    }
}
