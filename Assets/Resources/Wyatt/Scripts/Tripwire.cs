using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    public Transform endOne;
    public Transform endTwo;

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
                //code for sound effects
                Debug.Log("yeet");
                Destroy(gameObject);
                Debug.Log("yeet");

            }
        }
        Debug.DrawLine(endOne.position, endTwo.position, Color.black);
    }
}
