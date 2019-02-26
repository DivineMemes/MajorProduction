using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource bruh;
    AudioClip bruhClip;
    public SphereCollider soundCollider;


    private void Start()
    {
        bruh = gameObject.GetComponent<AudioSource>();
        bruhClip = bruh.clip;
    }
    private void OnTriggerEnter(Collider other)
    {
        bruh.PlayOneShot(bruhClip);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))

        {
            SphereCollider spawnedCollider = Instantiate(soundCollider, gameObject.transform.position, Quaternion.identity);
            spawnedCollider.tag = "Sound";

            //Destroy(gameObject);
            Destroy(spawnedCollider);
            AudioSource.PlayClipAtPoint(bruhClip, gameObject.transform.position);
        }

        
    }
}
