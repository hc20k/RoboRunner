using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // <-- has the functions needed to load scenes

public class NewSceneOnCameraCollision : MonoBehaviour
{
    public string sceneName = ""; // You can fill this in when you attach this script to an object in the inspector.

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Same idea with the other script, change TAG to the tag you made for the collider.
        if (other.gameObject.tag == "TAG")
        {
            // Loads the scene by it's name, and makes sure it's the only scene loaded.
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
