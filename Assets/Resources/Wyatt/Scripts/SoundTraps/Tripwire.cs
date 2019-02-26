using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    public Transform endOne;
    public Transform endTwo;
    public SphereCollider soundCollider;
    public float killTime = .5f;
    bool coroutineStarted;
    bool hasTriggered;

    void Start()
    {
        hasTriggered = false;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Linecast(endOne.position, endTwo.position, out hit))
        {
            if (hit.collider.tag == "Player"&&!hasTriggered)
            {
                SphereCollider spawnedCollider = Instantiate(soundCollider, hit.collider.transform.position, Quaternion.identity);
                spawnedCollider.tag = "Sound";
                hasTriggered = true;
                if (!coroutineStarted)
                {
                    StartCoroutine(waitfordelete(spawnedCollider));
                }
                
            }
        }
        Debug.DrawLine(endOne.position, endTwo.position, Color.black);
    }
    IEnumerator waitfordelete(SphereCollider collider)
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(killTime);
        coroutineStarted = false;
        Destroy(collider);
        Destroy(gameObject);
    }
}
