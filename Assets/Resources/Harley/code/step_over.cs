﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class step_over : MonoBehaviour {
    public float raylength = 10;
    RaycastHit hit;
    public Transform move;
    public GameObject player;
    public GameObject text;
    bool on;
    public float time;
    bool step;
    public float i = 0.0f;

    public float duration = 3.0f;
    public AnimationCurve heightChange;

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(0.5f);
        var rate = 8.0f / time;
        i += Time.deltaTime * rate;
    }

    IEnumerator DoStep()
    {
        Vector3 start = player.transform.position;
        Vector3 end = move.position;
        float amount = 0.0f;
       
        while(amount < duration)
        {
            amount += Time.deltaTime;

            float perc = amount / duration;
            player.transform.position = Vector3.Lerp(start, end, perc) + Vector3.up * heightChange.Evaluate(perc);
            player.SetActive(false);
            yield return null;
        }
        player.SetActive(true);
    }

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
                    //step = true;
                    //StartCoroutine(Wait2());
                    StartCoroutine(DoStep());
                }
            }
        }
        if(step == true)
        {
            //player.transform.position = Vector3.Lerp(player1.transform.position, move.position, i);
          
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
