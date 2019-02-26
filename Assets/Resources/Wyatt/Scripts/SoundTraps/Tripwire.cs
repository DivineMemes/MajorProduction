using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    public Transform endOne;
    public Transform endTwo;
    public SphereCollider soundCollider;

    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast(endOne.position, endTwo.position, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                SphereCollider spawnedCollider = Instantiate(soundCollider, hit.collider.transform.position, Quaternion.identity);
                spawnedCollider.tag = "Sound";

                Destroy(gameObject);
            }
        }
        Debug.DrawLine(endOne.position, endTwo.position, Color.black);
    }
}
