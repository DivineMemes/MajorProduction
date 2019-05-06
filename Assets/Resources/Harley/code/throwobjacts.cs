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
    public AudioSource sound;
    public AudioClip throwsoud;
    public AudioClip pickupsound;
    public AudioClip dropsound;
    public GameObject grabui;
    public GameObject grabui2;
    public GameObject normalui;
    public GameObject throwui;
    public controler control;
    public int dmg;
    IEnumerator cliderenble()
    {
        sound2.GetComponent<Collider>().enabled = true;
        sound.PlayOneShot(throwsoud);
        yield return new WaitForSeconds(timer);
        sound.Pause();
        sound2.GetComponent<Collider>().enabled = false;
        throwme = false;
    }
        //private bool touched = false;
        // Use this for initialization
        void Start ()
        {
        sound = gameObject.GetComponent<AudioSource>();
            
        }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.red);
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (!beingCarried)
        {
            if (dist <= 2.5f)
            {
                hasplayer = true;

            }
            if(dist > 2.5f)
            {
                hasplayer = false;

            }
            if (hasplayer && Input.GetKeyDown(KeyCode.E) && control.nomore == 0)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                sound2.GetComponent<Collider>().enabled = false;
                transform.parent = playerCam;
                sound.PlayOneShot(pickupsound);
                //gamemanger.GM.grab = true;
                //normalui.SetActive(false);
                //throwui.SetActive(true);
                gamemanger.GM.throwme = true;
                gamemanger.GM.thrownumber = 1;
                timer = 1;
                me.GetComponent<MeshRenderer>().enabled = false;
                beingCarried = true;
                control.nomore = 1;

            }
        }
        if (beingCarried)
        {
            hasplayer = false;
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
                //normalui.SetActive(true);
                //throwui.SetActive(false);
                //gamemanger.GM.grab = false;
                gamemanger.GM.throwme = false;
                throwme = true;
                //StartCoroutine(cliderenble());
                GetComponent<Rigidbody>().AddForce(playerCam.forward * throwforce);
                control.nomore = 0;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //normalui.SetActive(true);
                //throwui.SetActive(false);
                GetComponent<Rigidbody>().isKinematic = false;
                me.GetComponent<MeshRenderer>().enabled = true;
                gamemanger.GM.throwme = false;
                sound.PlayOneShot(dropsound);
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
        if (hasplayer)
        {
            gamemanger.GM.grab = true;
        }
        if(!hasplayer)
        {
            gamemanger.GM.grab = false;
        }
        if(gamemanger.GM.grab == true)
        {
         
                normalui.SetActive(false);
                throwui.SetActive(false);
                grabui.SetActive(true);
                if (gamemanger.GM.thrownumber == 0)
                {
                    grabui2.SetActive(true);
                }
       
        }
        if(gamemanger.GM.grab == false)
        {
            if(gamemanger.GM.throwme == true)
            {
                throwui.SetActive(true);
                grabui.SetActive(false);
                grabui2.SetActive(false);
            }
            if (gamemanger.GM.throwme == false)
            {
                throwui.SetActive(false);
                normalui.SetActive(true);
                grabui.SetActive(false);
                grabui2.SetActive(false);
            }
            if(gamemanger.GM.hide == true)
            {
                throwui.SetActive(false);
                normalui.SetActive(false);
            }
            if(gamemanger.GM.stepover == true)
            {
                throwui.SetActive(false);
                normalui.SetActive(false);
            }
           
        }
        //else
        //{
        //    hasplayer = false;
        //}
        
    }
    void OnCollisionEnter()
    {
        if (throwme)
        StartCoroutine(cliderenble());
    }
}
