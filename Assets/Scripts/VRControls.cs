using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRControls : MonoBehaviour
{
    Manager manager;

    public float movementRate = 5;
    public float jumpForce = 1;

    public GameObject controllerL;
    public GameObject controllerR;

    public SteamVR_Action_Boolean touchPadPressed;
    public SteamVR_Action_Boolean burgerPressed;

    public bool isGripping;
    public bool canMove = true;

    public SteamVR_Action_Vector2 touchPadSlide;
    public SteamVR_Action_Single triggerAction;

    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Manager>();
        weapon = GetComponentInChildren<Weapon>();

        triggerAction.AddOnChangeListener(WillShoot, SteamVR_Input_Sources.RightHand);
    }

    private void WillShoot(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta)
    {
        if(fromSource == SteamVR_Input_Sources.RightHand && newAxis > 0.9)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        // Shoot
        weapon.GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.8f,1.1f);
        weapon.GetComponent<AudioSource>().Play();

        GameObject bullet = Instantiate(manager.bullet, weapon.muzzle.transform.position, weapon.muzzle.transform.rotation);
        bullet.tag = "PlayerBullet";
        bullet.transform.Rotate(new Vector3(0, 90, 90));
        bullet.GetComponent<Rigidbody>().velocity = weapon.muzzle.transform.forward * 200;

        bullet.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f); // looks wierd w/o it
        bullet.GetComponent<Renderer>().enabled = true;

        yield return new WaitForSeconds(2);
        Destroy(bullet);

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Vector2 touchpadValue = touchPadSlide.GetAxis(SteamVR_Input_Sources.Any);
        bool isPressingBurger = burgerPressed.GetState(SteamVR_Input_Sources.Any);

        if(!isGripping && canMove == true)
        {
            transform.position += (mainCamera.transform.right * touchpadValue.x + mainCamera.transform.forward * touchpadValue.y) * Time.deltaTime * movementRate;
            bool isOnGround = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
            if (touchPadPressed.lastState && isOnGround == true)
            {
                // Jump
                GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
        }

        Time.timeScale = (isPressingBurger == true) ? 0.2f : 1f; // Matrix bitch
    }
}
