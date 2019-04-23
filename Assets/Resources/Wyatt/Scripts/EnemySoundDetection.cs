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
    public bool suprisedSound;
    //public AudioClip suprised;
    private AudioSource source;
    AudioMixer screech;

    private void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        positionRecorded = false;
    }

    void Update()
    {

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(!chasing.targetSpotted)
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
                     source.Play();
                     
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
