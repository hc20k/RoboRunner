using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class LevelSelector : MonoBehaviour
{
    public string sceneName = "";
    Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            manager.player.GetComponent<VRControls>().triggerAction.RemoveOnChangeListener(manager.player.GetComponent<VRControls>().WillShoot, SteamVR_Input_Sources.RightHand); //MissingReferenceException
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
