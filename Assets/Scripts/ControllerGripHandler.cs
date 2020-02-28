using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class ControllerGripHandler : MonoBehaviour
{
    GameObject body;
    public Vector3 prevPos;
    public SteamVR_Action_Boolean gripPressed;
    public int controllerIndex = 0; // 0 - left, 1 - right
    bool restrictGripping = false;
    public bool canGrip;


    // Start is called before the first frame update
    void Start()
    {
        body = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Manager>().player;
    }

    public void Ungrip()
    {
        body.GetComponent<Rigidbody>().useGravity = true;
        body.GetComponent<Rigidbody>().isKinematic = false;
        body.GetComponent<VRControls>().isGripping = false;
    }

    public IEnumerator UngripBecauseShot()
    {
        restrictGripping = true;
        yield return new WaitForSeconds(2);
        restrictGripping = false;
    }

    // Update is called once per frame
    void Update()
    {
        SteamVR_Input_Sources inputController = (controllerIndex == 0) ? SteamVR_Input_Sources.LeftHand : SteamVR_Input_Sources.RightHand;

        if(canGrip && gripPressed.GetState(inputController) && restrictGripping == false)
        {
            body.GetComponent<Rigidbody>().useGravity = false;
            body.GetComponent<Rigidbody>().isKinematic = true;
            body.transform.position += (prevPos - transform.localPosition);
            body.GetComponent<VRControls>().isGripping = true;

        } else if(canGrip && gripPressed.GetStateUp(inputController))
        {
            body.GetComponent<Rigidbody>().velocity = (prevPos - transform.localPosition) / Time.deltaTime;
            Ungrip();
        }
        else
        {
            Ungrip();
        }

        prevPos = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            canGrip = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            canGrip = false;
        }
    }
}
