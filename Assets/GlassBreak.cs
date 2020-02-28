using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Break());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Break()
    {
        foreach(Vector3 vector in GetComponent<MeshFilter>().mesh.vertices)
        {
            GameObject clone = Instantiate(gameObject, transform.position + vector, Quaternion.identity);
            clone.transform.localScale = transform.localScale / GetComponent<MeshFilter>().mesh.vertices.Length;
            clone.AddComponent<Rigidbody>();
        }

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "PlayerBullet")
        {
            // Break
            //StartCoroutine(Break());
        }
    }
}
