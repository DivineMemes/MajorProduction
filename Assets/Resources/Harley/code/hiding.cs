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
    public GameObject normalui;
    public float time;
    public float i = 0.0f;
    public Transform hidingspot;
    public ThirdPerson k;
    public Transform normelspot;
    public float duration = 3.0f;
    // Use this for initialization
    IEnumerator Wait2()
    {
        
        yield return new WaitForSeconds(0.5f);
        var rate = 8.0f / time;
        i += Time.deltaTime * rate;
        isHiding = true;
        guiShow = false;
        //time = 3;
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
            mm.controller.you.SetBool("walk", false);
            mm.controller.you.SetBool("run", false);
           
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
            mm.controller.you.SetBool("walk", false);
            mm.controller.you.SetBool("run", false);
            //isHiding = true;
            //guiShow = false;
            yield return null;
        }
        me.SetActive(true);
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
     

                //time = time -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Q))
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
                    StartCoroutine(dohid());




                }
            }
        }



        if (isHiding == true)
        {
            //if (maincam.transform.position == Vector3.Lerp(maincam.transform.position, hidingspot.position, i))
            //{
            //    maincam.transform.forward = hidingspot.transform.forward;
            //}
            if (Input.GetKeyDown(KeyCode.Q))
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
            hidui.SetActive(true);
            normalui.SetActive(false);
        }
        if (gamemanger.GM.hide == false)
        {
            hidui.SetActive(false);
            normalui.SetActive(true);
        }
        else
        {
            guiShow = false;
        }
    }
    
}
