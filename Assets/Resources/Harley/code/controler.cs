﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controler : MonoBehaviour {
    public float horizontal;
    public float vertical;
    public bool jump;
    public moiro play;
    public bool noinput;
    public Collider sound;
    public AudioSource me;
    public Animator you;
    public bool run;
    public bool light;
    public GameObject stop;
    public Light myflaselight;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gamemanger.GM.pause == true)
        {
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamemanger.GM.pause = false;
                stop.SetActive(false);
            }
            return;
        }
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            vertical = -1;
            you.ResetTrigger("idle");
            you.SetTrigger("walk");
            sound.enabled = true;
            noinput = false;
        }
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            vertical = 1;
            you.ResetTrigger("idle");
            you.SetTrigger("walk");
            sound.enabled = true;
            noinput = false;
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            horizontal = -1;
            you.ResetTrigger("idle");
            you.SetTrigger("walk");
            sound.enabled = true;
            noinput = false;
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKeyDown(KeyCode.A))
        {
            horizontal = 1;
            you.ResetTrigger("idle");
            you.SetTrigger("walk");
            sound.enabled = true;
            noinput = false;
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            horizontal = 0;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            vertical = 0;
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            noinput = true;
            you.ResetTrigger("walk");
            you.SetTrigger("idle");
            sound.enabled = false;
        }
        if (play.grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position += transform.up * play.jump * Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            light = !light;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamemanger.GM.pause = true;
            stop.SetActive(true);
        }
        
        if (light == true)
        {
            myflaselight.enabled = true;
        }
        if (light == false)
        {
            myflaselight.enabled = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            run = false;
        }
      
        if(gamemanger.GM.grab == true)
        {
            you.SetTrigger("grab");
        }
        if (gamemanger.GM.grab == false)
        {
            you.ResetTrigger("grab");
        }

    }
}
