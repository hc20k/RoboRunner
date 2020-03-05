using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoFade : MonoBehaviour
{
    // Start is called before the first frame update

    float scrollSpeed = 0.1f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = rend.material.mainTextureOffset.x;
        if(offset > 1) { offset = 0; }

        offset += 0.01f;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
