using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    public float regenSpeed = 5;
    public float regenAmount = 10;
    public int enemiesKilled = 0;

    public bool godMode = false;

    public AudioClip hurtSound;

    float lastDamageTime;

    public void OnLevelFinished(Manager.LevelOutcome outcome)
    {
        // TODO: Freeze player (remove character controller or equivalent)
        // TODO: Play music
        // TODO: Show statistics
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    void Die()
    {
        GameObject.FindWithTag("LevelManager").GetComponent<Manager>().Respawn();
        health = 100;
    }

    IEnumerator TookDamage()
    {
        // Oooooof
        GetComponent<AudioSource>().clip = hurtSound;
        GetComponent<AudioSource>().Play();

        // TODO: Set Vignette

        // Ungrip
        print("Player took damage. Health: " + health);
        GetComponent<VRControls>().controllerL.GetComponent<ControllerGripHandler>().UngripBecauseShot();
        GetComponent<VRControls>().controllerR.GetComponent<ControllerGripHandler>().UngripBecauseShot();

        if (health <= 0)
        {
            // U is dead foo
            Die();
            yield return null;
        }

        lastDamageTime = Time.time;

        yield return new WaitForSeconds(regenSpeed);

        if(Time.time - lastDamageTime > regenSpeed) // TODO:This line may need tweaking
        {
            while(health < maxHealth)
            {
                if(health + regenAmount > maxHealth)
                {
                    health = maxHealth;
                } else
                {
                    health += regenAmount;
                }
            }
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemiesKilled++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(godMode == true) { return; }
        // Bullet damage
        if(other.gameObject.tag == "Bullet")
        {
            health -= GameObject.FindWithTag("LevelManager").GetComponent<Manager>().bulletDamage;

            StartCoroutine(TookDamage());
            Destroy(other.gameObject);
        } else if(other.gameObject.tag == "PlayerBullet")
        {
            // Suicide. tf??
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
