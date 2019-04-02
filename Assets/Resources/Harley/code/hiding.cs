using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hiding : MonoBehaviour {
    public Camera maincam;
    public Camera hands ;
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
    public float time;
    public float i = 0.0f;
    public Transform hidingspot;
    public ThirdPerson k;
    // Use this for initialization
    IEnumerator Wait2()
    {
        
        yield return new WaitForSeconds(0.5f);
        var rate = 7.0f / time;
        i += Time.deltaTime * rate;
        isHiding = true;
        guiShow = false;
        //time = 3;
    }
    void Start ()
    {
        maincam.enabled = true;
        //hidingcam.enabled = false;
 ;
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
                if (Input.GetKeyDown(KeyCode.F))
                {
                    
                    //mm.hid = true;
                    k.enabled = false;
                    me.SetActive(false);
                   //hand.SetActive(false);
                    //maincam.enabled = false;
                    //hands.enabled = false;
                    //hidingcam.enabled = true;
                    StartCoroutine(Wait2());




                }
            }
        }



        if (isHiding == true)
        {
            maincam.transform.position = Vector3.Lerp(maincam.transform.position, hidingspot.position, i);
            if (maincam.transform.position == Vector3.Lerp(maincam.transform.position, hidingspot.position, i))
            {
                maincam.transform.forward = hidingspot.transform.forward;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                //mm.hid = false;
                k.enabled = true;
                me.SetActive(true);
                //hand.SetActive(true);
               // maincam.enabled = true;
                //hands.enabled = true;
                //hidingcam.enabled = false;
                
                isHiding = false;
                //you = false;

            }
        }

        if (guiShow == true)
        {
            hidui.SetActive(true);
        }
        if (guiShow == false)
        {
            hidui.SetActive(false);
        }
        else
        {
            guiShow = false;
        }
    }
    
}
