using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerActionScript : MonoBehaviour {

    // a reference to the action
    public SteamVR_Action_Boolean trigger;
    // a reference to the hand
    public SteamVR_Input_Sources handType;


    void Start() {
        trigger.AddOnStateDownListener(TriggerDown, handType);
        trigger.AddOnStateUpListener(TriggerUp, handType);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        Debug.Log("Trigger is up");
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        Debug.Log("Trigger is down");
    }

    // Update is called once per frame
    void Update() {

    }
}
