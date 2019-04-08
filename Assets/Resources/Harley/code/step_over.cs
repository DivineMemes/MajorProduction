using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class step_over : MonoBehaviour {
    public float raylength = 10;
    RaycastHit hit;
    public Transform move;
    public GameObject player;
    public GameObject text;
    bool on;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit, raylength))
        {
            if (hit.collider.tag == "step_over")
            {
              move = hit.collider.GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
                //print("hit player");
                on = true;
                if (Input.GetKeyDown(KeyCode.G))
                {
                    on = false;

                    player.transform.position = Vector3.Lerp(player.transform.position,move.position,1); 
                }
            }
        }
        if(on == true)
        {
            text.SetActive(true);
        }
        if (on == false)
        {
            text.SetActive(false);
        }
        else
        {
            on = false;
        }
    }

}
