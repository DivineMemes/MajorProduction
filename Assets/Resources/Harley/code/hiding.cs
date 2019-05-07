using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hiding : MonoBehaviour {
    public Camera maincam;
    public Camera hands ;
    public GameObject lookat;
    public GameObject hand;
    //public Camera hidingcam;
    public bool isHiding = false;
    public float raylength = 10;
    RaycastHit hit;
    public float turn;
    public bool guiShow = false;
    public moiro mm;
    public GameObject me;
    public GameObject hidui;
    public GameObject hidui2;
    public controler control;
    public int number;
    public GameObject normalui;
    public GameObject throwui;
    public float time;
    public float i = 0.0f;
    public Transform hidingspot;
    public Transform front;
    public ThirdPerson k;
    public Transform normelspot;
    public float duration = 3.0f;
    // Use this for initialization
    IEnumerator Wait2()
    {

        Vector3 start = me.transform.position;
        Vector3 end = front.position;
        float amount = 0.0f;
        while (amount < duration)
        {
            amount += Time.deltaTime;
            float perc = amount / duration;
            me.transform.position = Vector3.Lerp(start, end, perc);
            mm.enabled=false;
            control.you.SetBool("walk", true);
            yield return null;
        }
        StartCoroutine(dohid());
    }
  IEnumerator dohid()
    {
        Vector3 start = lookat.transform.position;
        Vector3 end = hidingspot.position;
        float amount = 0.0f;
       
        while (amount < duration)
        {
            amount += Time.deltaTime;
            float perc = amount / duration;
            lookat.transform.position = Vector3.Lerp(start, end, perc);
            me.SetActive(false);
            control.you.SetBool("walk", false);
           control.you.SetBool("run", false);
           
            yield return null;
        }
        isHiding = true;
        guiShow = false;
    }
    IEnumerator dounhid()
    {
        Vector3 start = lookat.transform.position;
        Vector3 end = normelspot.position;
        float amount = 0.0f;
       
        while (amount < duration)
        {
            amount += Time.deltaTime;
            float perc = amount / duration;
            lookat.transform.position = Vector3.Lerp(start, end, perc);
            //me.SetActive(false);
            control.you.SetBool("walk", false);
            control.you.SetBool("run", false);
            //isHiding = true;
            //guiShow = false;
            yield return null;
        }
        me.SetActive(true);
        mm.enabled = true;
        isHiding = false;
    }
    void Start ()
    {
        maincam.enabled = true;
        //hidingcam.enabled = false;
        
    }
	
	// Update is called once per frame
	void Update () {
     

        var fwd = transform.TransformDirection(Vector3.forward);

       
       

        if (Physics.Raycast(transform.position,fwd,out hit, raylength))
        {
           
            if (hit.collider.tag == "Hide"&&isHiding == false)
            {
                
                guiShow = true;
                hidingspot = hit.collider.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
                front = hit.collider.GetComponent<Transform>().GetChild(1).GetComponent<Transform>();

                //time = time -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //mm.controller.you.SetBool("walk", false);
                    //mm.controller.you.SetBool("run", false);
                    //mm.hid = true;
                    //k.enabled = false;
                    //me.SetActive(false);
                    //hand.SetActive(false);
                    //maincam.enabled = false;
                    //hands.enabled = false;
                    //hidingcam.enabled = true;
                    number += 1;
                    StartCoroutine(Wait2());




                }
            }
        }



        if (isHiding == true)
        {
            //if (maincam.transform.position == Vector3.Lerp(maincam.transform.position, hidingspot.position, i))
            //{
            //    maincam.transform.forward = hidingspot.transform.forward;
            //}
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                //mm.hid = false;
                //k.enabled = true;
                
                StartCoroutine(dounhid());
                //hand.SetActive(true);
                // maincam.enabled = true;
                //hands.enabled = true;
                //hidingcam.enabled = false;

                
                //you = false;

            }
        }

        if (guiShow == true)
        {
            gamemanger.GM.hide = true;
        }
        if (guiShow == false)
        {
            gamemanger.GM.hide = false;
        }
        if(gamemanger.GM.hide == true)
        {
            //hidui.SetActive(true);
            if (number == 0)
            {
                hidui2.SetActive(true);
            }
            if(gamemanger.GM.throwme == true)
            {
                normalui.SetActive(false);
                throwui.SetActive(false);
                hidui.SetActive(true);
            }
            if (gamemanger.GM.throwme == false)
            {
                normalui.SetActive(false);
                hidui.SetActive(true);
            }
        }
        if (gamemanger.GM.hide == false)
        {
            hidui.SetActive(false);
            if (gamemanger.GM.throwme == true)
            {
                throwui.SetActive(true);
                hidui.SetActive(false);
                hidui2.SetActive(false);
            }
            if (gamemanger.GM.throwme == false)
            {
                hidui2.SetActive(false);
                normalui.SetActive(true);
                hidui.SetActive(false);
            }
        }
        else
        {
            guiShow = false;
        }
    }
    
}
