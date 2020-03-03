using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Manager manager;
    public float health = 100;
    public float shotRate = 1;
    public bool canShoot = true;
    public float bulletLife = 2.0f;
    public bool deathBarrierApplies = true;

    public Transform enemyModelBase;

    public AudioClip fireSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip idleSound;

    public GameObject spotlight;

    public ParticleSystem fire;

    float lastShotTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        GetComponent<Rigidbody>().isKinematic = true; // DONT MOVE FROM BALLISTICS
        Idle();
    }

    void Idle()
    {
        GetComponent<AudioSource>().pitch = 1;
        GetComponent<AudioSource>().volume = 0.5f;
        GetComponent<AudioSource>().clip = idleSound;
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }

    void Die()
    {
        print("Enemy <" + gameObject + "> is dead.");

        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().pitch = 1;
        GetComponent<AudioSource>().clip = deathSound;
        GetComponent<AudioSource>().Play();

        // aight boys lets clean up
        GetComponent<Rigidbody>().isKinematic = false; // MOVE FROM BALLISTICS
        health = -1;
        fire.Stop();

        GetComponent<Rigidbody>().mass = 3;
        GetComponent<Hover>().canHover = false;
        spotlight.GetComponent<Light>().intensity = 0;
        manager.player.GetComponent<Player>().OnEnemyDeath(gameObject);

        // Zoom
        GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
    }

    public void OnPlayerRespawn()
    {
    
    }

    IEnumerator Shoot()
    {
        lastShotTime += Time.deltaTime;

        if (lastShotTime >= shotRate)
        {
            lastShotTime -= shotRate;

            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().pitch = 1;
            GetComponent<AudioSource>().clip = fireSound;
            GetComponent<AudioSource>().Play(); // pew

            GameObject newBullet = Instantiate(manager.bullet);
            newBullet.transform.position = gameObject.transform.position;
            newBullet.GetComponent<Rigidbody>().velocity = ((manager.player.transform.position + new Vector3(0,2,0)) - newBullet.transform.position).normalized * 50;
            newBullet.tag = "Bullet";

            Debug.Log(transform.name + " | Shoot");

            yield return new WaitForSeconds(bulletLife); // 2 second bullet life

            Destroy(newBullet);
        }
    }

    void Hurt()
    {

        if(health <= 0)
        {
            return; //ded
        }

        // Hurt sound
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.1f);
        GetComponent<AudioSource>().clip = hurtSound;
        GetComponent<AudioSource>().Play();

        health -= manager.bulletDamage;
        print("Enemy <" + gameObject + "> was hurt. Health = " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            Hurt();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            GetComponent<Hover>().canHover = !GetComponentInChildren<EnemyFOV>().canSeePlayer; // hover if it can't see player

            if (GetComponentInChildren<EnemyFOV>().canSeePlayer == true)
            {
                if (canShoot == true)
                {
                    spotlight.GetComponent<Light>().color = Color.red;
                    StartCoroutine(Shoot());
                }
                else
                    spotlight.GetComponent<Light>().color = Color.white;
            }
        }

        // Death barrier applies for you too
        if(transform.position.y < manager.deathBarrier && deathBarrierApplies)
        {
            if(health > 0)
            {
                Hurt(); // for sound lol
                Die();
            }

            gameObject.SetActive(false); // bye bye
        }
    }
}
