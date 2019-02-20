using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    //bool trapTriggered;
    //bool trapCoroutineStarted;
    //Chase chaseScript;
    //public float timer;
    //public GameObject trappedObj;
    void Start()
    {
        //trapTriggered = false;
    }

    private void Update()
    {

        //if (trapTriggered)
        //{
        //    if (!trapCoroutineStarted)
        //    {
        //        StartCoroutine(trapTime(trappedObj));
        //    }
        //}

    }

    private void OnTriggerEnter(Collider other)
    {

        /*if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            trappedObj = other.gameObject;
            trapTriggered = true;
        }*/

    }

    //IEnumerator trapTime(GameObject trappedObj)
    //{
    //    trapCoroutineStarted = true;
    //    if (trappedObj.CompareTag("Enemy"))
    //    {
    //        if (trappedObj.GetComponent<Chase>())
    //        {
    //            trappedObj.GetComponent<Chase>().enabled = false;
    //            trappedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //            yield return new WaitForSeconds(timer);
    //            Destroy(gameObject);
    //            trappedObj.GetComponent<Chase>().enabled = true;
    //        }
    //    }
    //    else if (trappedObj.CompareTag("Player"))
    //    {
    //        //Destroy(gameObject);
    //        //disable controller script and enable struggle script or activate struggle function
    //    }

    //    trapCoroutineStarted = false;
    //}


}
