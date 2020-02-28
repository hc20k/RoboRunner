using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class LaserTeleport : MonoBehaviour {

    // To work with the steam controllers we need to store references to them.
    // These two lines need to be at the top of every script you want to use
    // with the SteamVR controllers.

    // a reference to the action
    public SteamVR_Action_Boolean trigger;
    // a reference to the hand
    public SteamVR_Input_Sources handType;


    public LayerMask teleportMask;                     // This lets us enable/disable layers that we can teleport to

    public Transform headTransform;                    // Reference to where the user's head is

    public Transform cameraRigTransform;               // Reference to the center of the SteamVR area so
                                                       // we can compute an offset

    public GameObject teleportCrosshairsPrefab;        // reference to the crosshair prefab that we will make
    private GameObject crosshairs;                     // a variable to hold the actual crosshair that we DO make

    public GameObject laserPrefab;                     // reference to the laserbeam prefab that we will make
    private GameObject laser;                          // a variable to hold the actual laser that we DO make

    private Vector3 destination;                       // Variable to store the point we will teleport to

    private bool shouldTeleport;                       // A boolean variable to store the state of whether or not
                                                       // we should teleport (i.e. that our destination is valid)

    private bool isPressed = false;


    void Start() {
        laser = Instantiate(laserPrefab);
        crosshairs = Instantiate(teleportCrosshairsPrefab);
        laser.SetActive(false);
        crosshairs.SetActive(false);

        trigger.AddOnStateDownListener(TriggerDown, handType);
        trigger.AddOnStateUpListener(TriggerUp, handType);
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        Debug.Log("Trigger is up");
        isPressed = false;
        if (shouldTeleport) {
            Teleport(destination);
        }
    }
    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        Debug.Log("Trigger is down");
        isPressed = true;

    }

    void Update() {
        if (isPressed) {
            RaycastHit hit = new RaycastHit();

            bool validHit = Physics.Raycast(transform.position, transform.forward, out hit, 100);
            if (validHit) {
                if (teleportMask != (teleportMask | (1 << hit.transform.gameObject.layer))) {
                    validHit = false;
                }
            }

            if ( validHit ) {

                ShowLaser(hit);

                crosshairs.SetActive(true);
                crosshairs.transform.position = hit.point + new Vector3(0.0f, 0.05f, 0.0f);
                shouldTeleport = true;
                destination = hit.point;

            } else {
                shouldTeleport = false;
                laser.SetActive(false);
                crosshairs.SetActive(false);
            }
        } else {
            //shouldTeleport = false;
            laser.SetActive(false);
            crosshairs.SetActive(false);
        }
    }

    private void ShowLaser(RaycastHit hit) {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(transform.position, hit.point, .5f);
        laser.transform.LookAt(hit.point);
        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y, hit.distance);
    }

    private void Teleport(Vector3 destination) {
        shouldTeleport = false;
        crosshairs.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = destination + difference;
    }
}
