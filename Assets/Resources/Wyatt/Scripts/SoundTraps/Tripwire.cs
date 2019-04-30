using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    //public Transform endOne;
    //public Transform endTwo;

    public SphereCollider soundCollider;
    public float killTime = .5f;


    public AudioClip chimes;
    AudioSource source;

    bool coroutineStarted;
    bool hasTriggered;


    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        hasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasTriggered)
        {
            SphereCollider spawnedCollider = Instantiate(soundCollider, other.transform.position, Quaternion.identity);
            spawnedCollider.tag = "Sound";
            spawnedCollider.radius = 25;
            hasTriggered = true;
            source.PlayOneShot(chimes);
            if (!coroutineStarted)
            {
                StartCoroutine(waitfordelete(spawnedCollider));
            }
        }
    }
    IEnumerator waitfordelete(SphereCollider collider)
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(killTime);
        coroutineStarted = false;
        Destroy(collider);
        this.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
