using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    public Transform player;

    public Component[] meshRends;

    AudioSource source;

    bool pickedUp;
    bool inRange;
 
    int KeyCount;//temporary counter just incase we decide to use a scripted player block
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
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

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        if(!pickedUp)
        {
            gamemanger.GM.KeyCount++;
            pickedUp = true;
            meshRends = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer rend in meshRends)
            {
                rend.enabled = false;
            }
            StartCoroutine(fadeVolume());
        }
        //source.volume = Mathf.Lerp(0, source.volume, Time.deltaTime);
    }
    IEnumerator fadeVolume()
    {
        while(source.volume > .007)
        {
            source.volume = Mathf.Lerp(source.volume, 0, Time.deltaTime);
            yield return null;
        }
        StopCoroutine(fadeVolume());
        gameObject.SetActive(false);
    }
}
