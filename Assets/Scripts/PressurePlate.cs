using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] objectsToAffect;
    Vector3 origPos;
    Vector3[] origObjectPos;

    void Start()
    {
        origObjectPos = new Vector3[objectsToAffect.Length];
        origPos = transform.position;

        for(int i = 0; i < objectsToAffect.Length; i ++)
        {
            origObjectPos[i] = objectsToAffect[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        transform.position = origPos;
        for (int i = 0; i < objectsToAffect.Length; i++)
        {
            objectsToAffect[i].position = origObjectPos[i];
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Floor") { return; }

        Vector3 tmpPos = origPos;
        tmpPos.y -= 1;
        transform.position = tmpPos;

        // ------------------------------

        for (int i = 0; i < objectsToAffect.Length; i++)
        {
            Vector3 tmpObjPos = origObjectPos[i];
            tmpObjPos.y -= 100;
            objectsToAffect[i].position = tmpObjPos;
        }
    }
}
