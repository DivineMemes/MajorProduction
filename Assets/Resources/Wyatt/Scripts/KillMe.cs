using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    public float timeOfSound;
    AudioSource source;
    //AudioClip clip;

	// Use this for initialization
	void Start ()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 1;
        source.maxDistance = 10;
        source.dopplerLevel = 0;


        timeOfSound = 5.5f;
        StartCoroutine(playSound());
	}

    IEnumerator playSound()
    {
        source.Play();
        yield return new WaitForSeconds(timeOfSound);
        //Destroy(gameObject);
    }
}
