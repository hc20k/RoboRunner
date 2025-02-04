﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public bool doesSpin = true;
       
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    public bool canHover = true;

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    private void Update()
    {
        if(canHover)
        {
            if(doesSpin)
                transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }

}
