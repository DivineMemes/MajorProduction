using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    public Transform player;
    bool inRange;
    int KeyCount;//temporary counter just incase we decide to use a scripted player block
    void Start()
    {

    }


    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        gamemanger.GM.KeyCount++;
       
        Destroy(gameObject);
    }
}
