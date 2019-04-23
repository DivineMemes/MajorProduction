using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemySoundDetection : MonoBehaviour
{
    EnemySeek chasing;

    public Vector3 soundPos;
    public Vector3 soundPosTemp;
    public float radius;
    public bool heardSound = false;
    public bool searchingSound;
    public bool positionRecorded;
    private AudioSource source;
    public AudioClip suprised;
    public AudioClip whispers;
    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        chasing = gameObject.GetComponent<EnemySeek>();
        positionRecorded = false;
    }

    void Update()
    {

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!chasing.targetSpotted)
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
                    if (heardSound)
                    {
                        source.maxDistance = 10;
                        source.PlayOneShot(suprised);
                     
                    }
                }
                else
                {
                    heardSound = false;
                }
            }
        }
        if (!searchingSound)
        { 
            positionRecorded = false;
        }
    }
}
