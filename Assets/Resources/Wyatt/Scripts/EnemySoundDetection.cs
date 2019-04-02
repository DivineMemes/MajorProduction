using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundDetection : MonoBehaviour
{
    public Vector3 soundPos;
    public Vector3 soundPosTemp;
    public float radius;
    public bool heardSound = false;
    public bool searchingSound;
    public bool positionRecorded;


    private void Start()
    {
        positionRecorded = false;
    }

    void Update()
    {

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Sound" && !heardSound)
            {
                if(colliders[i].gameObject.GetComponent<Collider>().enabled == true)
                {
                    if (!positionRecorded)
                    {
                        soundPos = colliders[i].gameObject.GetComponent<Collider>().transform.position;
                        
                        positionRecorded = true;
                    }
                    if(positionRecorded)
                    {
                        soundPosTemp = soundPos;
                    }
                }
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
            //soundPos = null;
            //soundPosTemp = null;
            positionRecorded = false;
        }
    }

    void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(gameObject.transform.position, radius);
    }
}
