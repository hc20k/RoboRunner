using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int hits = 0;
    public ParticleSystem spark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator EndBulletLife()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    IEnumerator DoSparkAtTransform(Transform transf)
    {
        // Spark
        ParticleSystem sparkInstance = Instantiate(spark, transform.position, transform.rotation);
        sparkInstance.Play();

        yield return new WaitForSeconds(1);

        Destroy(sparkInstance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits++;
        // Spark
        ParticleSystem sparkInstance = Instantiate(spark, transform.position, transform.rotation);
        sparkInstance.Play();

        if (hits > 2)
        {
            EndBulletLife();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
