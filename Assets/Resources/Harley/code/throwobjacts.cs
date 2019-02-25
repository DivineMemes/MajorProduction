using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwobjacts : MonoBehaviour {
    public Transform player;
    public Transform playerCam;
    public GameObject me;
    public float throwforce = 10;
    bool hasplayer = false;
    bool beingCarried = false;
    public int dmg;
    //private bool touched = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f)
        {
            hasplayer = true;

        }
        else
        {
            hasplayer = false;
        }
        if(hasplayer && Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam;
            me.GetComponent<MeshRenderer>().enabled = false;
            beingCarried = true;
        }
        if (beingCarried)
        {
            //if (touched)
            //{
            //    GetComponent<Rigidbody>().isKinematic = false;
            //    transform.parent = null;
            //    beingCarried = false;
            //    touched = false;
            //}
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                me.GetComponent<MeshRenderer>().enabled = true;
                beingCarried = false;
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwforce);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                me.GetComponent<MeshRenderer>().enabled = true;
                transform.parent = null;
                beingCarried = false;
            }
        }
	}
}
