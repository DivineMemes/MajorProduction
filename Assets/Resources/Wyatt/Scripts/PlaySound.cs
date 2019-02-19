using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    AudioSource bruh;
    AudioClip bruhClip;


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
            AudioSource.PlayClipAtPoint(bruhClip, gameObject.transform.position);
        }

        
    }
}
