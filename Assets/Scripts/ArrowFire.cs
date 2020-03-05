using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ArrowFire : MonoBehaviour
{
    public GameObject arrow;
    public SteamVR_Action_Single triggerAction;
    public int speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        triggerAction.AddOnChangeListener(WillShoot, SteamVR_Input_Sources.RightHand);
    }

    public void WillShoot(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta)
    {
        if(fromSource == SteamVR_Input_Sources.RightHand && newAxis > 0.9)
        {
            GameObject newArrow = Instantiate(arrow, transform);
            newArrow.tag = "Arrow";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
