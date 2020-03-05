using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Weapon : MonoBehaviour
{
    public Transform muzzle;
    public GameObject glockSlide;

    public Material[] camos;
    public bool[] camosAnimate;
    public float scrollSpeed = 0.1f;

    int currentCamoIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        updateCamo();
    }

    void updateCamo()
    {
        if (glockSlide && camos[currentCamoIndex])
        {
            glockSlide.GetComponent<Renderer>().material = camos[currentCamoIndex];
        }
    }

    public void revolveCamo()
    {
        if(currentCamoIndex >= camos.Length-1)
        {
            currentCamoIndex = 0;
        } else
        {
            currentCamoIndex++;
        }

        updateCamo();
    }

    // Update is called once per frame
    void Update()
    {
        if(camosAnimate.Length >= currentCamoIndex && camosAnimate[currentCamoIndex] == true)
        {
            float offset = glockSlide.GetComponent<Renderer>().material.mainTextureOffset.x;
            if (offset > 1) { offset = 0; }

            offset += 0.01f;
            glockSlide.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        } else
        {
            glockSlide.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, 0));
        }
    }
}
