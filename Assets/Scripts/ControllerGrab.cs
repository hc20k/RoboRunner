using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(BoxCollider))]
public class ControllerGrab : MonoBehaviour {
    // a reference to the action
    public SteamVR_Action_Boolean trigger;
    // a reference to the hand
    public SteamVR_Input_Sources handType;
    private SteamVR_Behaviour_Pose handPose;
    public SteamVR_Action_Vibration vibration;

    private GameObject collidingObject;
    private GameObject objectInHand;
    private bool triggerHeld = false;



    void Start() {
        trigger.AddOnStateDownListener(TriggerDown, handType);
        trigger.AddOnStateUpListener(TriggerUp, handType);
        handPose = GetComponent<SteamVR_Behaviour_Pose>();
        BoxCollider bc = GetComponent<BoxCollider>();
        bc.center = new Vector3(0f, -0.05f, 0.03f);
        bc.size = new Vector3(0.1f, 0.1f, 0.1f);
        bc.isTrigger = true;
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        triggerHeld = false;
        Release();
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        triggerHeld = true;
        if (collidingObject) {
            Grab();
        }
    }

    // Update is called once per frame
    void LateUpdate() {
        if (triggerHeld) {
            UpdateObjectPos();
        }
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entered " + other.gameObject.name);
        collidingObject = other.gameObject;
        //Controller.TriggerHapticPulse();
        if (!objectInHand) {
            vibration.Execute(0, .1f, 3, 1, handType);
        }
    }

    public void OnTriggerStay(Collider other) {
        collidingObject = other.gameObject;
    }

    public void OnTriggerExit(Collider other) {
        Debug.Log("Trigger exited " + other.gameObject.name);
        collidingObject = null;
    }

    void Grab() {
        if (objectInHand) {
            return;
        }

        objectInHand = collidingObject;
        collidingObject = null;

        UpdateObjectPos();
    }

    void Release() {
        if (objectInHand == null) {
            return;
        }

        objectInHand.GetComponent<Rigidbody>().velocity = handPose.GetVelocity();
        objectInHand.GetComponent<Rigidbody>().angularVelocity = handPose.GetAngularVelocity();

        objectInHand = null;
    }

    void UpdateObjectPos() {
        if (objectInHand == null) {
            return;
        }

        objectInHand.transform.rotation = gameObject.transform.rotation;
        objectInHand.transform.position = gameObject.transform.position + gameObject.transform.forward * .1f;
    }
}
