using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public int force = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            // send it
            other.gameObject.GetComponent<Rigidbody>().AddForce(0, force, 0, ForceMode.Impulse);
            GetComponent<AudioSource>().Play();
        }
    }
}
