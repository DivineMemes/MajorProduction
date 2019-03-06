using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundDetection : MonoBehaviour
{
    public Transform soundPos;
    public Transform soundPosTemp;
    public float radius;
    public bool heardSound = false;
    public bool searchingSound;
    public bool recordPos;


    private void Start()
    {
        
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Sound" && colliders[i].gameObject.GetComponent<Collider>().enabled && !heardSound)
            {
                //if(colliders[i].gameObject.GetComponent<Collider>().enabled == true)
                //{
                    soundPos = colliders[i].gameObject.GetComponent<Collider>().transform;
                //}
                heardSound = true;
                searchingSound = true;
            }
            else
            {
                heardSound = false;
            }
        }
        if (!searchingSound)
        {
            soundPosTemp = null;
        }
    }
}
