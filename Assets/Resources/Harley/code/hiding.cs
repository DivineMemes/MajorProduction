using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hiding : MonoBehaviour {
    public Camera maincam;
    public Camera hands;
    public GameObject hand;
    public Camera hidingcam;
    public bool isHiding = false;
    public float raylength = 10;
    RaycastHit hit;
    public bool guiShow = false;
    public moiro mm;
    public GameObject me;
    public GameObject hidui;
    // Use this for initialization
    IEnumerator WAIT2()
    {
        yield return new WaitForSeconds(0.5f);
        isHiding = true;
        guiShow = false;
    }
    void Start () {
        maincam.enabled = true;
        hidingcam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (guiShow == true)
        {
            hidui.SetActive(true);
        }
        if (guiShow == false)
        {
            hidui.SetActive(false);
        }
        var fwd = transform.TransformDirection(Vector3.forward);
        if(Physics.Raycast(transform.position,fwd,out hit, raylength))
        {
            if(hit.collider.tag == "Hide"&&isHiding == false)
            {
                guiShow = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //mm.hid = true;
                    me.SetActive(false);
                    hand.SetActive(false);
                    maincam.enabled = false;
                    hands.enabled = false;
                    hidingcam.enabled = true;
                    isHiding = true;
                    WAIT2();
                    
                }
            }
        }
        else
        {
            guiShow = false;
        }
        if(isHiding == true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                //mm.hid = false;
                me.SetActive(true);
               hand.SetActive(true);
                maincam.enabled = true;
                hands.enabled = false;
                hidingcam.enabled = false;
                isHiding = false;
                
            }
        }
       
	}
}
