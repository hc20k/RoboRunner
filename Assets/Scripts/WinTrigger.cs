using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    Manager manager;
    public GameObject parentLabelGameObject;
    public GameObject timeLabel;
    public GameObject attemptsLabel;
    public GameObject enemiesKilledLabel;

    void Start()
    {
        parentLabelGameObject.SetActive(false);
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && manager.levelFinished == false)
        {
            manager.LevelFinished(Manager.LevelOutcome.WIN);
            GetComponentInChildren<ParticleSystem>().Play();

            // Setup labels
            parentLabelGameObject.SetActive(true);

            // Setup time
            System.TimeSpan duration = manager.stopwatch.Elapsed;

            timeLabel.GetComponent<TextMesh>().text = "Time: "+ string.Format("{0:D2}:{1:D2}",duration.Minutes, duration.Seconds);
            attemptsLabel.GetComponent<TextMesh>().text = "Attempts: "+manager.attempts;
            enemiesKilledLabel.GetComponent<TextMesh>().text = "Enemies Killed: " + manager.player.GetComponent<Player>().enemiesKilled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
