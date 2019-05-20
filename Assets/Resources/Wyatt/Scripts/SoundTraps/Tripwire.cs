using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    //public Transform endOne;
    //public Transform endTwo;

    

    public SphereCollider soundCollider;
    public float killTime = .5f;
    GameObject soundPos;

    public AudioClip chimes;
    public AudioClip snap;
    AudioSource source;
    public controler control;
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
            if(control.crouch == true)
            {
                return;
            }
            if (gamemanger.GM.onetime == 0)
            {
                gamemanger.GM.sneak = true;
                //control.me2.clip = control.voice;
                control.me2.PlayOneShot(control.voice);
                control.sneak.SetActive(true);
            }
            //source.PlayOneShot(chimes);
            SphereCollider spawnedCollider = Instantiate(soundCollider, other.transform.position, Quaternion.identity);
            spawnedCollider.tag = "Sound";
            spawnedCollider.radius = 25;
            soundPos = new GameObject("soundActivater");
            AudioSource sound = soundPos.AddComponent<AudioSource>();
            sound.clip = chimes;
            soundPos.transform.position = gameObject.transform.position;
            soundPos.AddComponent<KillMe>();
            

            hasTriggered = true;
            source.PlayOneShot(snap);
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
