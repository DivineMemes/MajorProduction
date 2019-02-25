using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundDetection : MonoBehaviour
{
    public float radius;
    //public LayerMask sound;
    public bool heardSound = false;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.tag== "Sound")
            {
                heardSound = true;
            }
        }
    }



}
