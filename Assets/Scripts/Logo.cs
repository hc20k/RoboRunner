using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Light logoLight in GetComponentsInChildren<Light>())
        {
            logoLight.intensity += Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.01f;

            Vector3 lTmpPos = logoLight.transform.position;
            lTmpPos.z += Mathf.Sin(Time.fixedTime * Mathf.PI * 0.4f) * 0.05f;
            logoLight.transform.position = lTmpPos;
        }
    }
}
