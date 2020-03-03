using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Manager : MonoBehaviour
{
    [Header("Essentials")]
    Vector3 spawnpoint;
    public GameObject player;
    public int deathBarrier = 0;

    [Header("Ballistics")]
    public GameObject bullet;
    public float bulletDamage = 25;

    [HideInInspector]
    public bool levelFinished;
    public System.Diagnostics.Stopwatch stopwatch;
    public int attempts = 1;

    [Header("Level Specific")]
    public string levelID; // for saving

    public enum LevelOutcome { WIN, FAIL };

    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new System.Diagnostics.Stopwatch(); // i know i know i could just import Diagnostics but idc
        stopwatch.Start();

        try
        {
            spawnpoint = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        }
        catch
        {
            Debug.LogError("NO SPAWN PLATFORM IN SCENE");
        }
    }

    private void Save()
    {
        // Write level completed
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate);

        // TODO: Save
    }

    public void Respawn()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                enemy.GetComponent<Enemy>().OnPlayerRespawn();
            }
        }

        if(levelFinished == false)
        {
            player.transform.position = spawnpoint + new Vector3(0, 3, 0);
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); //reset veloc
            attempts++;
        } else
        {
            player.transform.position = GameObject.FindGameObjectWithTag("Win").transform.position;
        }

    }

    public void LevelFinished(LevelOutcome outcome)
    {
        player.GetComponent<VRControls>().canMove = false;

        stopwatch.Stop();

        levelFinished = true;

        if(outcome == LevelOutcome.WIN)
        {
            player.GetComponent<Player>().OnLevelFinished(outcome);
        }

        Save();
    }

    // Update is called once per frame
    void Update()
    {

        // death barrier

        if(player.transform.position.y < deathBarrier)
        {
            Respawn();
        }
    }
}