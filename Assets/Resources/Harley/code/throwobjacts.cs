using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwobjacts : MonoBehaviour {
    public Transform player;
    public Transform playerCam;
    public GameObject me;
    public float throwforce = 10;
   public bool hasplayer = false;
    public GameObject sound2;
    bool beingCarried = false;
    public bool throwme;
    public float height = 0.5f;
    public float heightpadding = 0.05f;
    public LayerMask ground;
    RaycastHit hitInfo;
    public AnimationCurve velocityCurve;
    public float timer;
    public controler control;
    public int dmg;
    IEnumerator cliderenble()
    {
        sound2.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(timer);
        sound2.GetComponent<Collider>().enabled = false;
        throwme = false;
    }
        //private bool touched = false;
        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.red);
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 2.5f)
        {
            hasplayer = true;

        }
        else
        {
            hasplayer = false;
        }
        if(hasplayer && Input.GetKeyDown(KeyCode.E)&&control.nomore == 0)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            sound2.GetComponent<Collider>().enabled = false;
            transform.parent = playerCam;
            //gamemanger.GM.grab = true;
            timer = 1;
            me.GetComponent<MeshRenderer>().enabled = false;
            beingCarried = true;
            control.nomore = 1;
           
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
                //sound2.GetComponent<Collider>().enabled = true;
                gamemanger.GM.grab = false;
                throwme = true;
                //StartCoroutine(cliderenble());
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwforce);
                control.nomore = 0;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                me.GetComponent<MeshRenderer>().enabled = true;
                transform.parent = null;
                beingCarried = false;
                control.nomore = 0;
            }
            //if (Physics.Raycast(transform.position, -transform.up, out hitInfo, height + heightpadding, ground))
            //{
            //    if (Vector3.Distance(transform.position, hitInfo.point) == ground)
            //    {
            //        if (throwme == true)
            //        {
            //            sound2.GetComponent<Collider>().enabled = true;
            //            throwme = false;
            //        }
            //    }
            //}
            //if (throwme == false)
            //{
            //    sound2.GetComponent<Collider>().enabled = false;
            //}


        }
        
    }
    void OnCollisionEnter()
    {
        if (throwme)
        StartCoroutine(cliderenble());
    }
}
